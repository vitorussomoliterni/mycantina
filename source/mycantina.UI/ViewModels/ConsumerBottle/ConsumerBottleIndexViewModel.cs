using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.ConsumerBottle
{
    public class ConsumerBottleIndexViewModel
    {
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public int ConsumerId { get; set; }
        [Display(Name = "Bottle Name")]
        public string WineName { get; set; }
        [Display(Name = "Format")]
        public string WineFormat { get; set; }
        [Display(Name = "Quantity")]
        public int QtyOwned { get; set; }
        [Display(Name = "In your collection")]
        public Boolean Owned { get; set; }
        [Display(Name = "Price Paid")]
        public decimal PricePaid { get; set; }
    }
}