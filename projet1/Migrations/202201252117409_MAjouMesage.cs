namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MAjouMesage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        date = c.String(),
                        conversationId = c.Int(nullable: false),
                        fromClient = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.conversations", t => t.conversationId, cascadeDelete: true)
                .Index(t => t.conversationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.messages", "conversationId", "dbo.conversations");
            DropIndex("dbo.messages", new[] { "conversationId" });
            DropTable("dbo.messages");
        }
    }
}
