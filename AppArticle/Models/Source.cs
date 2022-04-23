using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppArticle.Models
{
    public class Source
    {
        [Key]
        public string link { get; set; }
        public string superSelector { get; set; }
        public string titleSelector { get; set; }
        public string descriptionSelector { get; set; }
        public string imgSelector { get; set; }
        public string contentSelector { get; set; }
    }
}