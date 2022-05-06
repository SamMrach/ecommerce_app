namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mlabelhist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.historiques", "label", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.historiques", "label");
        }
    }
}
