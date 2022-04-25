using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotArticle.Entity
{
    class Article
    {
        public string link_detail { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string thumnail { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }

        public override string ToString()
        {
            return link_detail + "," + title;
        }
    }
}
