using System;
using System.Collections.Generic;

namespace mycantina.DataAccess.Models
{
    public class WineFormat
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<ConsumerBottle> ConsumerBottles { get; set; }
    }
}