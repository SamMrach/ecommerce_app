namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuppMessage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "emetteurId", "dbo.utilisateurs");
            DropForeignKey("dbo.Messages", "recepteurId", "dbo.utilisateurs");
            DropIndex("dbo.Messages", new[] { "emetteurId" });
            DropIndex("dbo.Messages", new[] { "recepteurId" });
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        emetteurId = c.Int(nullable: false),
                        recepteurId = c.Int(),
                        Id = c.Int(nullable: false, identity: true),
                        text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Messages", "recepteurId");
            CreateIndex("dbo.Messages", "emetteurId");
            AddForeignKey("dbo.Messages", "recepteurId", "dbo.utilisateurs", "id");
            AddForeignKey("dbo.Messages", "emetteurId", "dbo.utilisateurs", "id", cascadeDelete: true);
        }
    }
}
