namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mfinition5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.utilisateurs", "password", c => c.String());
            AlterColumn("dbo.utilisateurs", "passwordComfirm", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.utilisateurs", "passwordComfirm", c => c.String());
            AlterColumn("dbo.utilisateurs", "password", c => c.String(nullable: false));
        }
    }
}
