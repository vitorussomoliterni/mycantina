using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.Review
{
    public class ReviewDetailsViewModel
    {
        [ScaffoldColumn(false)]
        public int ConsumerId { get; set; }
        [ScaffoldColumn(false)]
        public int BottleId { get; set; }
        [Display(Name = "Bottle Name")]
        public string BottleName { get; set; }
        public String Country { get; set; }
        public String Region { get; set; }
        [Display(Name = "Wine Type")]
        public String WineType { get; set; }
        public int Year { get; set; }
        public String Producer { get; set; }
        [Display(Name = "Average Price")]
        public decimal AvgPrice { get; set; }
        public string Description { get; set; }
        [Display(Name = "Review")]
        public string Text { get; set; }
        public int Rating { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime DatePosted { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime? DateModified { get; set; }
    }
}