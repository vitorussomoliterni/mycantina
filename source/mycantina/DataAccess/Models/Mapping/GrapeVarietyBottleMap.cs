using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class GrapeVarietyBottleMap : EntityTypeConfiguration<GrapeVarietyBottle>
    {
        public GrapeVarietyBottleMap()
        {
            this.HasKey(k => new { k.GrapeVarietyId, k.BottleId });
        }
    }
}
