using AppArticle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace AppArticle.Data
{
    public class Context : DbContext
    {
        public Context() : base("DemoApp")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<CategoryArticle> CategoryArticles { get; set; }
        public DbSet<Source> Sources { get; set; }
  


    }
}