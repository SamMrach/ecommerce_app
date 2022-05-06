namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removevalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.produits", "title", c => c.String());
            AlterColumn("dbo.produits", "description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.produits", "description", c => c.String(nullable: false));
            AlterColumn("dbo.produits", "title", c => c.String(nullable: false));
        }
    }
}
