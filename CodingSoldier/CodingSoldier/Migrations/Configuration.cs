namespace CodingSoldier.Migrations
{
    using CodingSoldier.Core.Models;    
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingSoldier.Core.Models.CodingSoldierDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodingSoldier.Core.Models.CodingSoldierDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Categories.AddOrUpdate(
                category => category.CategoryName,
                new Category { CategoryName = "C#.NET" },
                new Category { CategoryName = "JavaScript" },
                new Category { CategoryName = "ASP.NET" },
                new Category { CategoryName = "SQL" },
                new Category { CategoryName = "MVC" }
            );            
        }
    }
}
