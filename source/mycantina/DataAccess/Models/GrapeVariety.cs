using System;
using System.Collections.Generic;

namespace mycantina.DataAccess.Models
{
    public class GrapeVariety
    {
        public GrapeVariety()
        {
            Bottles = new List<Bottle>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Bottle> Bottles { get; set; }
    }
}