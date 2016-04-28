using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class WineFormatMap : EntityTypeConfiguration<WineFormat>
    {
        public WineFormatMap()
        {
            this.Property(p => p.Name)
                .HasColumnType("varchar")
                .IsRequired();
        }
    }
}
