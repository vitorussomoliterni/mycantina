using System;
using System.Collections.Generic;

namespace mycantina.DataAccess.Models
{
    public class Bottle
    {
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
        public List<User_Bottle> User_Bottles { get; set; }
        public List<Review> Reviews { get; set; }
        public List<GrapeVariety_Bottle> GrapeVariety_Bottles { get; set; }
    }
}