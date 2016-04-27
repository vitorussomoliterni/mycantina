using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class GrapeVarietyMap : EntityTypeConfiguration<GrapeVariety>
    {
        public GrapeVarietyMap()
        {
            this.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
