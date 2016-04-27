using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class BottleMap : EntityTypeConfiguration<Bottle>
    {
        public BottleMap()
        {
            this.Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            this.Property(p => p.Region)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            this.Property(p => p.Country)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(p => p.Type)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(p => p.Year)
                .IsRequired();

            this.Property(p => p.Producer)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            this.Property(p => p.Description)
                .HasColumnType("nvarchar");

            this.Property(p => p.MinPrice)
                .HasColumnType("money");

            this.Property(p => p.MaxPrice)
                .HasColumnType("money");
        }
    }
}
