namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.utilisateurs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                        prenom = c.String(),
                        email = c.String(),
                        tel = c.String(),
                        CIN = c.String(),
                        SIREN = c.String(),
                        type_activite = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        cat_Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.cat_Id);
            
            CreateTable(
                "dbo.produits",
                c => new
                    {
                        CategorieId = c.Int(nullable: false),
                        CommandeId = c.Int(nullable: false),
                        ref_Id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        prix = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ref_Id)
                .ForeignKey("dbo.categories", t => t.CategorieId, cascadeDelete: true)
                .ForeignKey("dbo.commandes", t => t.CommandeId, cascadeDelete: true)
                .Index(t => t.CategorieId)
                .Index(t => t.CommandeId);
            
            CreateTable(
                "dbo.commandes",
                c => new
                    {
                        reference_cmd = c.Int(nullable: false, identity: true),
                        date = c.String(),
                        montant = c.Double(nullable: false),
                        quantite = c.Int(nullable: false),
                        clientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.reference_cmd)
                .ForeignKey("dbo.utilisateurs", t => t.clientId, cascadeDelete: true)
                .Index(t => t.clientId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        emetteurId = c.Int(nullable: false),
                        recepteurId = c.Int(),
                        Id = c.Int(nullable: false, identity: true),
                        text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.utilisateurs", t => t.emetteurId, cascadeDelete: true)
                .ForeignKey("dbo.utilisateurs", t => t.recepteurId)
                .Index(t => t.emetteurId)
                .Index(t => t.recepteurId);
            
            CreateTable(
                "dbo.reclamations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        header = c.String(),
                        text = c.String(),
                        CommandeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.commandes", t => t.CommandeId, cascadeDelete: true)
                .Index(t => t.CommandeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.reclamations", "CommandeId", "dbo.commandes");
            DropForeignKey("dbo.Messages", "recepteurId", "dbo.utilisateurs");
            DropForeignKey("dbo.Messages", "emetteurId", "dbo.utilisateurs");
            DropForeignKey("dbo.produits", "CommandeId", "dbo.commandes");
            DropForeignKey("dbo.commandes", "clientId", "dbo.utilisateurs");
            DropForeignKey("dbo.produits", "CategorieId", "dbo.categories");
            DropIndex("dbo.reclamations", new[] { "CommandeId" });
            DropIndex("dbo.Messages", new[] { "recepteurId" });
            DropIndex("dbo.Messages", new[] { "emetteurId" });
            DropIndex("dbo.commandes", new[] { "clientId" });
            DropIndex("dbo.produits", new[] { "CommandeId" });
            DropIndex("dbo.produits", new[] { "CategorieId" });
            DropTable("dbo.reclamations");
            DropTable("dbo.Messages");
            DropTable("dbo.commandes");
            DropTable("dbo.produits");
            DropTable("dbo.categories");
            DropTable("dbo.utilisateurs");
        }
    }
}
