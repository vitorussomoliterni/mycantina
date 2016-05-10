using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.Review
{
    public class ReviewCreateViewModel
    {
        [ScaffoldColumn(false)]
        public int ConsumerId { get; set; }
        [ScaffoldColumn(false)]
        public int BottleId { get; set; }
        [Display(Name = "Write a Review")]
        public string Text { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}