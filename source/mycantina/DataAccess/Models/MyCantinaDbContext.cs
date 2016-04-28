using mycantina.DataAccess.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models
{
    class MyCantinaDbContext : DbContext
    {
        public DbSet<Bottle> Bottles { get; set; }
        public DbSet<GrapeVariety> GrapeVarieties { get; set; }
        public DbSet<GrapeVariety_Bottle> GrapeVariety_Bottles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Bottle> User_Bottles { get; set; }
        public DbSet<WineFormat> WineFormats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BottleMap());
            modelBuilder.Configurations.Add(new GrapeVariety_BottleMap());
            modelBuilder.Configurations.Add(new GrapeVarietyMap());
            modelBuilder.Configurations.Add(new ReviewMap());
            modelBuilder.Configurations.Add(new User_BottleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new WineFormatMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
