namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.utilisateurs", "passwordComfirm", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.utilisateurs", "passwordComfirm", c => c.String());
        }
    }
}
