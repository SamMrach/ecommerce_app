namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MutilisateruLabel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.reclamations", name: "proprietaireId", newName: "utilisateurId");
            RenameIndex(table: "dbo.reclamations", name: "IX_proprietaireId", newName: "IX_utilisateurId");
            AddColumn("dbo.commandes", "label", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.commandes", "label");
            RenameIndex(table: "dbo.reclamations", name: "IX_utilisateurId", newName: "IX_proprietaireId");
            RenameColumn(table: "dbo.reclamations", name: "utilisateurId", newName: "proprietaireId");
        }
    }
}
