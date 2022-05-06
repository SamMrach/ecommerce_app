namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mecommerce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.reclamations", "clientId", c => c.Int(nullable: false));
            AlterColumn("dbo.utilisateurs", "nom", c => c.String(nullable: false));
            AlterColumn("dbo.utilisateurs", "prenom", c => c.String(nullable: false));
            AlterColumn("dbo.utilisateurs", "email", c => c.String(nullable: false));
            AlterColumn("dbo.utilisateurs", "tel", c => c.String(nullable: false));
            CreateIndex("dbo.reclamations", "clientId");
            AddForeignKey("dbo.reclamations", "clientId", "dbo.utilisateurs", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.reclamations", "clientId", "dbo.utilisateurs");
            DropIndex("dbo.reclamations", new[] { "clientId" });
            AlterColumn("dbo.utilisateurs", "tel", c => c.String());
            AlterColumn("dbo.utilisateurs", "email", c => c.String());
            AlterColumn("dbo.utilisateurs", "prenom", c => c.String());
            AlterColumn("dbo.utilisateurs", "nom", c => c.String());
            DropColumn("dbo.reclamations", "clientId");
        }
    }
}
