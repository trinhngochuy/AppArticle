namespace AppArticle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_articles : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Articles");
            AddColumn("dbo.Articles", "link_detail", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Articles", "link_detail");
            DropColumn("dbo.Articles", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Articles");
            DropColumn("dbo.Articles", "link_detail");
            AddPrimaryKey("dbo.Articles", "id");
        }
    }
}
