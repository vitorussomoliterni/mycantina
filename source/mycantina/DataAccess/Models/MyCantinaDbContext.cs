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
        public DbSet<GrapeVarietyBottle> GrapeVarietyBottles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<ConsumerBottle> ConsumerBottles { get; set; }
        public DbSet<WineFormat> WineFormats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BottleMap());
            modelBuilder.Configurations.Add(new GrapeVarietyBottleMap());
            modelBuilder.Configurations.Add(new GrapeVarietyMap());
            modelBuilder.Configurations.Add(new ReviewMap());
            modelBuilder.Configurations.Add(new ConsumerBottleMap());
            modelBuilder.Configurations.Add(new ConsumerMap());
            modelBuilder.Configurations.Add(new WineFormatMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
