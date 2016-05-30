using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mycantina.UI.ViewModels.Review
{
    public class ReviewEditViewModel
    {
        [ScaffoldColumn(false)]
        public int ConsumerId { get; set; }
        [ScaffoldColumn(false)]
        public int BottleId { get; set; }
        [Display(Name = "Review")]
        public string Text { get; set; }
        [Required]
        public int Rating { get; set; }
        public SelectList Ratings { get; set; }
    }
}