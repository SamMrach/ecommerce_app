namespace projet1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.produits", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.produits", "ImagePath");
        }
    }
}
