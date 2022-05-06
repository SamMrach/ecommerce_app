namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mfinission1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.produits", "CommandeId", "dbo.commandes");
            DropIndex("dbo.produits", new[] { "CommandeId" });
            CreateTable(
                "dbo.commande_produit",
                c => new
                    {
                        cmd_id = c.Int(nullable: false),
                        produit_id = c.Int(nullable: false),
                        id = c.Int(nullable: false, identity: true),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.commandes", t => t.cmd_id, cascadeDelete: true)
                .ForeignKey("dbo.produits", t => t.produit_id, cascadeDelete: true)
                .Index(t => t.cmd_id)
                .Index(t => t.produit_id);
            
            CreateTable(
                "dbo.conversations",
                c => new
                    {
                        clientId = c.Int(nullable: false),
                        proprietaireId = c.Int(nullable: false),
                        id = c.Int(nullable: false, identity: true),
                        LastMessage = c.String(),
                        LastMessageDate = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.utilisateurs", t => t.clientId, cascadeDelete: true)
                .ForeignKey("dbo.utilisateurs", t => t.proprietaireId, cascadeDelete: false)
                .Index(t => t.clientId)
                .Index(t => t.proprietaireId);
            
            CreateTable(
                "dbo.paniers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        somme = c.Double(nullable: false),
                        client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.utilisateurs", t => t.client_Id, cascadeDelete: true)
                .Index(t => t.client_Id);
            
            CreateTable(
                "dbo.panier_produit",
                c => new
                    {
                        panier_id = c.Int(nullable: false),
                        produit_id = c.Int(nullable: false),
                        id = c.Int(nullable: false, identity: true),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.paniers", t => t.panier_id, cascadeDelete: true)
                .ForeignKey("dbo.produits", t => t.produit_id, cascadeDelete: true)
                .Index(t => t.panier_id)
                .Index(t => t.produit_id);
            
            AddColumn("dbo.utilisateurs", "password", c => c.String());
            AddColumn("dbo.utilisateurs", "address", c => c.String());
            AddColumn("dbo.utilisateurs", "status", c => c.Int());
            AddColumn("dbo.produits", "subtitle", c => c.String());
            AddColumn("dbo.reclamations", "proprietaireId", c => c.Int(nullable: false));
            AddColumn("dbo.reclamations", "status", c => c.String());
            AlterColumn("dbo.produits", "title", c => c.String(nullable: false));
            CreateIndex("dbo.reclamations", "proprietaireId");
            AddForeignKey("dbo.reclamations", "proprietaireId", "dbo.utilisateurs", "id", cascadeDelete: false);
            DropColumn("dbo.produits", "CommandeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.produits", "CommandeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.reclamations", "proprietaireId", "dbo.utilisateurs");
            DropForeignKey("dbo.panier_produit", "produit_id", "dbo.produits");
            DropForeignKey("dbo.panier_produit", "panier_id", "dbo.paniers");
            DropForeignKey("dbo.paniers", "client_Id", "dbo.utilisateurs");
            DropForeignKey("dbo.conversations", "proprietaireId", "dbo.utilisateurs");
            DropForeignKey("dbo.conversations", "clientId", "dbo.utilisateurs");
            DropForeignKey("dbo.commande_produit", "produit_id", "dbo.produits");
            DropForeignKey("dbo.commande_produit", "cmd_id", "dbo.commandes");
            DropIndex("dbo.reclamations", new[] { "proprietaireId" });
            DropIndex("dbo.panier_produit", new[] { "produit_id" });
            DropIndex("dbo.panier_produit", new[] { "panier_id" });
            DropIndex("dbo.paniers", new[] { "client_Id" });
            DropIndex("dbo.conversations", new[] { "proprietaireId" });
            DropIndex("dbo.conversations", new[] { "clientId" });
            DropIndex("dbo.commande_produit", new[] { "produit_id" });
            DropIndex("dbo.commande_produit", new[] { "cmd_id" });
            AlterColumn("dbo.produits", "title", c => c.String());
            DropColumn("dbo.reclamations", "status");
            DropColumn("dbo.reclamations", "proprietaireId");
            DropColumn("dbo.produits", "subtitle");
            DropColumn("dbo.utilisateurs", "status");
            DropColumn("dbo.utilisateurs", "address");
            DropColumn("dbo.utilisateurs", "password");
            DropTable("dbo.panier_produit");
            DropTable("dbo.paniers");
            DropTable("dbo.conversations");
            DropTable("dbo.commande_produit");
            CreateIndex("dbo.produits", "CommandeId");
            AddForeignKey("dbo.produits", "CommandeId", "dbo.commandes", "reference_cmd", cascadeDelete: true);
        }
    }
}
