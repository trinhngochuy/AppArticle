using BotSource.Entity;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BotSource
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ListArticleLink = new List<string>();
            String sql = "SELECT * FROM Sources";
            String sqlar = "SELECT link_detail FROM [Articles]";
            List<Source> sources = new List<Source>();

            using (SqlConnection cnn = ConectionHelper.getConection())
            {
                try
                {
                    cnn.Open();
                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Source source = new Source()
                                {
                                    link = reader.GetString(0),
                                    titleSelector = reader.GetString(1),
                                    descriptionSelector = reader.GetString(2),
                                    imgSelector = reader.GetString(3),
                                    contentSelector = reader.GetString(4),
                                    superSelector = reader.GetString(5),
                                };
                                sources.Add(source);
                            }
                        }
                        using (SqlCommand commandline = new SqlCommand(sqlar, cnn))
                        {
                            using (SqlDataReader reader = commandline.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string articlelink = reader.GetString(0);
                                    ListArticleLink.Add(articlelink);
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            };

            foreach (var source in sources)
            {
                HashSet<string> ListString = new HashSet<string>();
                HashSet<Source> ListSubSource = new HashSet<Source>();
                var url = source.link;
                var web = new HtmlWeb();
                var doc = web.Load(url);
                string seleclink = "." + source.superSelector.Replace(" ", ".") + " a";
                foreach (var linkz in doc.QuerySelectorAll(seleclink))
                {
                    ListString.Add(linkz.GetAttributeValue("href", null));
                }
                var result = ListString.Except(ListArticleLink).ToArray();
                for (int i = 0; i < result.Length; i++)
                {
                   var re = result[i];
                    
                
                    if (i >0)
                    {
                        var rebe = result[i - 1];

                        if (!re.Contains(rebe))
                        {
                            Source subsource = new Source()
                            {
                                link = re,
                                titleSelector = source.titleSelector,
                                descriptionSelector = source.descriptionSelector,
                                imgSelector = source.imgSelector,
                                contentSelector = source.contentSelector,
                            };
                            Console.WriteLine(re);
                            //ListSubSource.Add(subsource);
                            var factory = new ConnectionFactory() { HostName = "localhost" };
                            using (var connection = factory.CreateConnection())
                            using (var channel = connection.CreateModel())
                            {
                                channel.QueueDeclare(queue: "SubSource",
                                                     durable: false,
                                                     exclusive: false,
                                                     autoDelete: false,
                                                     arguments: null);
                                var yourObject = JsonConvert.SerializeObject(subsource);
                                var body = Encoding.UTF8.GetBytes(yourObject);
                                channel.BasicPublish(exchange: "",
                                           routingKey: "SubSource",
                                           basicProperties: null,
                                           body: body);

                            }
                        }
                    }
                    else {
                        Source subsource = new Source()
                        {
                            link = re,
                            titleSelector = source.titleSelector,
                            descriptionSelector = source.descriptionSelector,
                            imgSelector = source.imgSelector,
                            contentSelector = source.contentSelector,
                        };
                        Console.WriteLine(re);
                        //ListSubSource.Add(subsource);
                        var factory = new ConnectionFactory() { HostName = "localhost" };
                        using (var connection = factory.CreateConnection())
                        using (var channel = connection.CreateModel())
                        {
                            channel.QueueDeclare(queue: "SubSource",
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);
                            var yourObject = JsonConvert.SerializeObject(subsource);
                            var body = Encoding.UTF8.GetBytes(yourObject);
                            channel.BasicPublish(exchange: "",
                                       routingKey: "SubSource",
                                       basicProperties: null,
                                       body: body);

                        }
                    }
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}

