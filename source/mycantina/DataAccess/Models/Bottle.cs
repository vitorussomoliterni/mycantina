using System;
using System.Collections.Generic;

namespace mycantina.DataAccess.Models
{
    public class Bottle
    {
        public Bottle()
        {
            Reviews = new List<Review>();
            GrapeVarietyBottles = new List<GrapeVarietyBottle>();
            ConsumerBottle = new List<ConsumerBottle>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Region { get; set; }
        public String Country { get; set; }
        public String Type { get; set; }
        public DateTime Year { get; set; }
        public String Producer { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string GrapeVariety { get; set; }
        public List<ConsumerBottle> ConsumerBottle { get; set; }
        public List<Review> Reviews { get; set; }
        public List<GrapeVarietyBottle> GrapeVarietyBottles { get; set; }
    }
}