namespace mycantina.Migrations
{
    using DataAccess.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<mycantina.DataAccess.Models.MyCantinaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(mycantina.DataAccess.Models.MyCantinaDbContext context)
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

            context.GrapeVarieties.AddOrUpdate(
                p => p.Name,
                new GrapeVariety { Name = "Aglianico" },
                new GrapeVariety { Name = "Barbera" },
                new GrapeVariety { Name = "Pinot Noir" },
                new GrapeVariety { Name = "Cabernet Sauvignon" },
                new GrapeVariety { Name = "Lambrusco" },
                new GrapeVariety { Name = "Malbec" },
                new GrapeVariety { Name = "Merlot" },
                new GrapeVariety { Name = "Montepulciano" },
                new GrapeVariety { Name = "Nebbiolo" },
                new GrapeVariety { Name = "Negroamaro" },
                new GrapeVariety { Name = "Nero D'Avola" },
                new GrapeVariety { Name = "Pinotage" },
                new GrapeVariety { Name = "Sangiovese" },
                new GrapeVariety { Name = "Chianti" },
                new GrapeVariety { Name = "Shiraz" },
                new GrapeVariety { Name = "Cabernet" },
                new GrapeVariety { Name = "Tempranillo" },
                new GrapeVariety { Name = "Chardonnay" },
                new GrapeVariety { Name = "Falanghina" },
                new GrapeVariety { Name = "Fiano" },
                new GrapeVariety { Name = "Greco Bianco" },
                new GrapeVariety { Name = "Malvasia" },
                new GrapeVariety { Name = "Moscato" },
                new GrapeVariety { Name = "Pecorino" },
                new GrapeVariety { Name = "Pinot Gris" },
                new GrapeVariety { Name = "Pinot Grigio" },
                new GrapeVariety { Name = "Prosecco" },
                new GrapeVariety { Name = "Ribolla Gialla" },
                new GrapeVariety { Name = "Riesling" },
                new GrapeVariety { Name = "Sauvignon Blanc" },
                new GrapeVariety { Name = "Sémillon" },
                new GrapeVariety { Name = "Trebbiano" },
                new GrapeVariety { Name = "Verdelho" }
                );
        }
    }
}
