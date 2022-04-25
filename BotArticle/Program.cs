using BotArticle.Entity;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotArticle
{
    class Program
    {
        static void Main(string[] args)
        {
   
            List<SubSource> ListSource = new List<SubSource>();
         
             String sql = "INSERT INTO Articles (title,description,thumnail,content,created_at,categoryArticle_id,link_detail) VALUES (@title, @description, @thumnail, @content,@created_at,@categoryArticle_id,@link_detail)";
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (SqlConnection cnn = ConectionHelper.getConection())
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                cnn.Open();
                channel.QueueDeclare(queue: "SubSource",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                 consumer.Received += (model, ea) =>
                {
                    HashSet<Article> ListArticle = new HashSet<Article>(new LinkComparer());
                    var body = ea.Body.ToArray();
                     ListSource = JsonConvert.DeserializeObject<List<SubSource>>(Encoding.UTF8.GetString(body));
              
                    foreach (var article in ListSource)
                    {
               
                        List<string> contentArticle = new List<string>();
                        var url = article.link;
                        var web = new HtmlWeb();
                        var doc = web.Load(url);
                        var title = doc.QuerySelector("." + article.titleSelector);
                        var description = doc.QuerySelector("." + article.descriptionSelector);
                        HtmlNode img = doc.QuerySelector("." + article.imgSelector + " img");
                        var contents = doc.QuerySelectorAll("." + article.contentSelector);
                        if (img!=null)
                        {
                            
                            Article subarticle = new Article()
                            {
                                link_detail = article.link,
                                title = title.InnerHtml,
                                description = description.InnerHtml,
                                thumnail = img.GetAttributeValue("data-src", string.Empty),

                            };
                            foreach (var conttent in contents)
                            {
                                contentArticle.Add(conttent.InnerHtml);
                            }
                            subarticle.content = JsonConvert.SerializeObject(contentArticle);
                            ListArticle.Add(subarticle);
                         /*       SqlCommand command = new SqlCommand(sql, cnn);
                                command.Parameters.Add("@link_detail", subarticle.link_detail);
                                command.Parameters.Add("@title", subarticle.title);
                                command.Parameters.Add("@description", subarticle.description);
                                command.Parameters.Add("@thumnail", subarticle.thumnail);
                                command.Parameters.Add("@created_at", DateTime.Now);
                                command.Parameters.Add("@content", subarticle.content);
                                command.Parameters.Add("@categoryArticle_id", 1);
                                command.ExecuteNonQuery();*/


                            /*   try
                                {
                                    DataTable tbl = new DataTable();
                                    tbl.Columns.Add(new DataColumn("title", typeof(string)));
                                    tbl.Columns.Add(new DataColumn("description", typeof(string)));
                                    tbl.Columns.Add(new DataColumn("thumnail", typeof(string)));
                                    tbl.Columns.Add(new DataColumn("content", typeof(string)));
                                    tbl.Columns.Add(new DataColumn("created_at", typeof(DateTime)));
                                    tbl.Columns.Add(new DataColumn("categoryArticle_id", typeof(Int32)));
                                    tbl.Columns.Add(new DataColumn("link_detail", typeof(string)));
                                    foreach (var arti in ListArticle)
                                    {
                                        DataRow dr = tbl.NewRow();
                                        dr["title"] = arti.title;
                                        dr["description"] = arti.description;
                                        dr["thumnail"] = arti.thumnail;
                                        dr["content"] = arti.content;
                                        dr["created_at"] = DateTime.Now;
                                        dr["categoryArticle_id"] = 1;
                                        dr["link_detail"] = arti.link_detail;
                                        tbl.Rows.Add(dr);
                                    }
                                    SqlBulkCopy objbulk = new SqlBulkCopy(cnn);
                                    objbulk.DestinationTableName = "Articles";
                                    objbulk.ColumnMappings.Add("title", "title");
                                    objbulk.ColumnMappings.Add("description", "description");
                                    objbulk.ColumnMappings.Add("thumnail", "thumnail");
                                    objbulk.ColumnMappings.Add("content", "content");
                                    objbulk.ColumnMappings.Add("created_at", "created_at");
                                    objbulk.ColumnMappings.Add("categoryArticle_id", "categoryArticle_id");
                                    objbulk.ColumnMappings.Add("link_detail", "link_detail");
                                    objbulk.WriteToServer(tbl);
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine(e.ToString());
                                }*/

                        }
                    };
                    Console.WriteLine(ListArticle.Count);
                    foreach(var ar in ListArticle)
                    {
                        SqlCommand command = new SqlCommand(sql, cnn);
                        command.Parameters.Add("@link_detail", ar.link_detail);
                        command.Parameters.Add("@title", ar.title);
                        command.Parameters.Add("@description", ar.description);
                        command.Parameters.Add("@thumnail", ar.thumnail);
                        command.Parameters.Add("@created_at", DateTime.Now);
                        command.Parameters.Add("@content", ar.content);
                        command.Parameters.Add("@categoryArticle_id", 1);
                        command.ExecuteNonQuery();
                    }
                    //Console.WriteLine(ListSource.Count()); sẽ không chạy được nếu viết ngoài hàm recived vì lý do revied :v là một luồng khác chạy chậm hơn các luồn ngoài
                };


            
       
                
                channel.BasicConsume(queue: "SubSource",
                          autoAck: true,
                          consumer: consumer);
            Console.WriteLine(" Press [enter] to exitw.");
            Console.ReadLine();

        
    }
}
    }
}
       