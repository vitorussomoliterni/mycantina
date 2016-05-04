using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.User_Bottle
{
    public class User_BottleDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Format")]
        public string WineFormat { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime? DateAcquired { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Quantity")]
        public int QtyOwned { get; set; }
        [Display(Name = "In your collection")]
        public Boolean Owned { get; set; }
        [Display(Name = "Price Paid")]
        public decimal PricePaid { get; set; }
    }
}