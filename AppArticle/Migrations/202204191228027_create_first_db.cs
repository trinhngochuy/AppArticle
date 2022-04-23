namespace AppArticle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_first_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        thumnail = c.String(),
                        content = c.String(),
                        created_at = c.DateTime(nullable: false),
                        categoryArticle_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CategoryArticles", t => t.categoryArticle_id)
                .Index(t => t.categoryArticle_id);
            
            CreateTable(
                "dbo.CategoryArticles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        link = c.String(),
                        titleSelector = c.String(),
                        descriptionSelector = c.String(),
                        imgSelector = c.String(),
                        contentSelector = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "categoryArticle_id", "dbo.CategoryArticles");
            DropIndex("dbo.Articles", new[] { "categoryArticle_id" });
            DropTable("dbo.Sources");
            DropTable("dbo.CategoryArticles");
            DropTable("dbo.Articles");
        }
    }
}
