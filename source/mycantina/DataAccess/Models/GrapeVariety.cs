using System;
using System.Collections.Generic;

namespace mycantina.DataAccess.Models
{
    public class GrapeVariety
    {
        public GrapeVariety()
        {
            GrapeVarietyBottles = new List<GrapeVarietyBottle>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public List<GrapeVarietyBottle> GrapeVarietyBottles { get; set; }
    }
}