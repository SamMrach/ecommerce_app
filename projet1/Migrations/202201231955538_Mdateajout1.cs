namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mdateajout1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.utilisateurs", "dateAjout", c => c.DateTime(nullable: true));
            DropColumn("dbo.utilisateurs", "dateAjout1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.utilisateurs", "dateAjout1", c => c.DateTime());
            AlterColumn("dbo.utilisateurs", "dateAjout", c => c.DateTime());
        }
    }
}
