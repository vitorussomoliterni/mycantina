using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.Bottle
{
    public class BottleIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Country { get; set; }
        [Display(Name = "Grape Variety")]
        public string GrapeVariety { get; set; }
        [Display(Name = "Wine Type")]
        public String Type { get; set; }
        public DateTime Year { get; set; }
        public String Producer { get; set; }
        [Display(Name = "Average Price")]
        public decimal AvgPrice { get; set; }
    }
}