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
            ConsumerBottles = new List<ConsumerBottle>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public int Year { get; set; }
        public String Producer { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int GrapeVarietyId { get; set; }
        public int WineTypeId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public virtual WineType WineType { get; set; }
        public virtual Country Country { get; set; }
        public virtual Region Region { get; set; }
        public List<ConsumerBottle> ConsumerBottles { get; set; }
        public List<Review> Reviews { get; set; }
        public virtual List<GrapeVarietyBottle> GrapeVarietyBottles { get; set; }
    }
}