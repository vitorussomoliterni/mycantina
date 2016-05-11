using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.Review
{
    public class ReviewIndexViewModel
    {
        [ScaffoldColumn(false)]
        public int ConsumerId { get; set; }
        [ScaffoldColumn(false)]
        public int BottleId { get; set; }
        [Display(Name = "Bottle Name")]
        public string BottleName { get; set; }
        [Display(Name = "Review")]
        public string Text { get; set; }
        public int Rating { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime DatePosted { get; set; }
    }
}