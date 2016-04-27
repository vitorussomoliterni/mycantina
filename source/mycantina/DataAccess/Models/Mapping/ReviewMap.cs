using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            this.Property(p => p.Text)
                .HasColumnType("nvarchar");

            this.Property(p => p.Rating)
                .IsRequired();

            this.Property(p => p.DatePosted)
                .IsRequired();

            this.HasKey(k => new { k.UserId, k.BottleId }); // Composite Key
        }
    }
}
