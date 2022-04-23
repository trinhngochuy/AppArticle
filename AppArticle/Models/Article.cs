using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppArticle.Models
{
    public class Article
    {
        [Key]
        public string link_detail { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string thumnail { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }

        public CategoryArticle categoryArticle { get; set; }
    }
}