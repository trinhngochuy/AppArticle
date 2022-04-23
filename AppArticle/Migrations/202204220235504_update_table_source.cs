namespace AppArticle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_source : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Sources");
            AlterColumn("dbo.Sources", "link", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Sources", "link");
            DropColumn("dbo.Sources", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sources", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Sources");
            AlterColumn("dbo.Sources", "link", c => c.String());
            AddPrimaryKey("dbo.Sources", "id");
        }
    }
}
