namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MdateOperation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.historiques", "dateOperation", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.historiques", "dateOperation");
        }
    }
}
