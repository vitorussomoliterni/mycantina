using mycantina.DataAccess.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models
{
    public class MyCantinaDbContext : DbContext
    {
        public DbSet<Bottle> Bottles { get; set; }
        public DbSet<GrapeVariety> GrapeVarieties { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<ConsumerBottle> ConsumerBottles { get; set; }
        public DbSet<WineFormat> WineFormats { get; set; }
        public DbSet<WineType> WineTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BottleMap());
            modelBuilder.Configurations.Add(new GrapeVarietyMap());
            modelBuilder.Configurations.Add(new ReviewMap());
            modelBuilder.Configurations.Add(new ConsumerBottleMap());
            modelBuilder.Configurations.Add(new ConsumerMap());
            modelBuilder.Configurations.Add(new WineFormatMap());
            modelBuilder.Configurations.Add(new WineTypeMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new RegionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
