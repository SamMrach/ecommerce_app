namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mdateajout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.utilisateurs", "dateAjout", c => c.DateTime());
            AddColumn("dbo.utilisateurs", "dateAjout1", c => c.DateTime());
            AddColumn("dbo.produits", "QtéStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.produits", "QtéStock");
            DropColumn("dbo.utilisateurs", "dateAjout1");
            DropColumn("dbo.utilisateurs", "dateAjout");
        }
    }
}
