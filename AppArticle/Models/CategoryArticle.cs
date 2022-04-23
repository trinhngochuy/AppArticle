using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppArticle.Models
{
    public class CategoryArticle
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<Article> articles { get; set; }
    }
}