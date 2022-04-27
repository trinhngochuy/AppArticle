using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppArticle.Models
{
    public class Article
    {
        [Display(Name = "CAM LINK")]
        [Key]
        public string link_detail { get; set; }
        [Display(Name = "CAM TITLE")]
        public string title { get; set; }
        [Display(Name = "CAM DESCRIPTION")]
        public string description { get; set; }
        [Display(Name = "CAM THUMBNAIL")]
        public string thumnail { get; set; }
        public string content { get; set; }
        [Display(Name = "CAM CREATED")]
        public DateTime created_at { get; set; }

        public CategoryArticle categoryArticle { get; set; }
    }
}