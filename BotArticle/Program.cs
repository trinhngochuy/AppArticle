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
         
            SubSource Source = new SubSource();
          
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
                  
                    var body = ea.Body.ToArray();
                     Source = JsonConvert.DeserializeObject<SubSource>(Encoding.UTF8.GetString(body));
              
                  
               
                        List<string> contentArticle = new List<string>();
                        var url = Source.link;
                        var web = new HtmlWeb();
                        var doc = web.Load(url);
                        var title = doc.QuerySelector("." + Source.titleSelector);
                        var description = doc.QuerySelector("." + Source.descriptionSelector);
                        HtmlNode img = doc.QuerySelector("." + Source.imgSelector + " img");
                        var contents = doc.QuerySelectorAll("." + Source.contentSelector);
                        if (img!=null)
                        {
                            Article subarticle = new Article()
                            {
                                link_detail = Source.link,
                                title = title.InnerHtml,
                                description = description.InnerHtml,
                                thumnail = img.GetAttributeValue("data-src", string.Empty),

                            };
                            foreach (var conttent in contents)
                            {
                                contentArticle.Add(conttent.InnerHtml);
                            }
                            subarticle.content = JsonConvert.SerializeObject(contentArticle);

                        SqlCommand command = new SqlCommand(sql, cnn);
                        command.Parameters.Add("@link_detail", subarticle.link_detail);
                        command.Parameters.Add("@title", subarticle.title);
                        command.Parameters.Add("@description", subarticle.description);
                        command.Parameters.Add("@thumnail", subarticle.thumnail);
                        command.Parameters.Add("@created_at", DateTime.Now);
                        command.Parameters.Add("@content", subarticle.content);
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
       