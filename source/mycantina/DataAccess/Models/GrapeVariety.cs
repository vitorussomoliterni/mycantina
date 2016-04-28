using System;
using System.Collections.Generic;

namespace mycantina.DataAccess.Models
{
    public class GrapeVariety
    {
        public GrapeVariety()
        {
            GrapeVariety_Bottles = new List<GrapeVariety_Bottle>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public List<GrapeVariety_Bottle> GrapeVariety_Bottles { get; set; }
    }
}