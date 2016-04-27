using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.Property(p => p.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(p => p.MiddleNames)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            this.Property(p => p.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(p => p.DateOfBirth)
                .IsRequired();

            this.Property(p => p.Email)
                .HasColumnType("varchar")
                .IsRequired();
        }
    }
}
