namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mdbase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.historiques",
                c => new
                    {
                        Id_hist = c.Int(nullable: false, identity: true),
                        operation = c.Int(nullable: false),
                        utilisateurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_hist)
                .ForeignKey("dbo.utilisateurs", t => t.utilisateurId, cascadeDelete: true)
                .Index(t => t.utilisateurId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.historiques", "utilisateurId", "dbo.utilisateurs");
            DropIndex("dbo.historiques", new[] { "utilisateurId" });
            DropTable("dbo.historiques");
        }
    }
}
