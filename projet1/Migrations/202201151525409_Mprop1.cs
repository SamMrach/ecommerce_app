namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mprop1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.produits", "dateAjout", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.produits", "dateAjout");
        }
    }
}
