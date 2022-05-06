namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.produits", "description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.produits", "description", c => c.String());
        }
    }
}
