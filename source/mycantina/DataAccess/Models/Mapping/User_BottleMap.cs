using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class User_BottleMap : EntityTypeConfiguration<User_Bottle>
    {
        public User_BottleMap()
        {
            this.Property(p => p.UserId)
                .IsRequired();

            this.Property(p => p.BottleId)
                .IsRequired();

            this.Property(p => p.WineFormatId)
                .IsRequired();

            this.Property(p => p.Owned)
                .HasColumnType("bit")
                .IsRequired();

            this.Property(p => p.PricePaid)
                .HasColumnType("money");
        }
    }
}
