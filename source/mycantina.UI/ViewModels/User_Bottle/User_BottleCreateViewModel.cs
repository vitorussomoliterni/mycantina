using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mycantina.UI.ViewModels.User_Bottle
{
    public class User_BottleCreateViewModel
    {
        [Required]
        [Display(Name = "Format")]
        public string WineFormat { get; set; }
        public SelectList WineFormats { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateAcquired { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Quantity")]
        public int QtyOwned { get; set; }
        [Required]
        [Display(Name = "Is it in your collection?")]
        public Boolean Owned { get; set; }
        [Display(Name = "Price Paid")]
        public decimal PricePaid { get; set; }
    }
}