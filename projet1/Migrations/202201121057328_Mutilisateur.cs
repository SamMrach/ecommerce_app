namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mutilisateur : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.produits", "utilisateurId", c => c.Int(nullable: false));
            CreateIndex("dbo.produits", "utilisateurId");
            AddForeignKey("dbo.produits", "utilisateurId", "dbo.utilisateurs", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.produits", "utilisateurId", "dbo.utilisateurs");
            DropIndex("dbo.produits", new[] { "utilisateurId" });
            DropColumn("dbo.produits", "utilisateurId");
        }
    }
}
