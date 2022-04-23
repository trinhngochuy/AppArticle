namespace AppArticle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_source_21_4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sources", "superSelector", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sources", "superSelector");
        }
    }
}
