using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models.Mapping
{
    class GrapeVariety_BottleMap : EntityTypeConfiguration<GrapeVariety_Bottle>
    {
        public GrapeVariety_BottleMap()
        {
            this.HasKey(k => new { k.GrapeVarietyId, k.BottleId });
        }
    }
}
