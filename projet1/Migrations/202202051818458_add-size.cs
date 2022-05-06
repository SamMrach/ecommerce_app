namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.panier_produit", "size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.panier_produit", "size");
        }
    }
}
