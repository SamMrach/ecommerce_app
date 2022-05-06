namespace projet1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<projet1.ModelBD>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "projet1.ModelBD";
        }

        protected override void Seed(projet1.ModelBD context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
