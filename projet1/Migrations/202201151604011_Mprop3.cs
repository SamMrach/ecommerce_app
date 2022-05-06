namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mprop3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.produits", "statusP", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.produits", "statusP");
        }
    }
}
