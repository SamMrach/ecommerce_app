namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.utilisateurs", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.utilisateurs", "Role");
        }
    }
}
