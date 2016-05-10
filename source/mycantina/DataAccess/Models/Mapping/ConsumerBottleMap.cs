using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class ConsumerBottleMap : EntityTypeConfiguration<ConsumerBottle>
    {
        public ConsumerBottleMap()
        {
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
