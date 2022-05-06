namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mfinission2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.utilisateurs", "passwordComfirm", c => c.String(nullable: false));
            AlterColumn("dbo.utilisateurs", "password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.utilisateurs", "password", c => c.String());
            DropColumn("dbo.utilisateurs", "passwordComfirm");
        }
    }
}
