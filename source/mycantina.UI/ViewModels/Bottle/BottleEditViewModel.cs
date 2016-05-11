using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mycantina.UI.ViewModels.Bottle
{
    public class BottleEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Bottle Name")]
        public string Name { get; set; }
        public String Region { get; set; }
        public SelectList Regions { get; set; }
        [Required]
        public String Country { get; set; }
        public SelectList Countries { get; set; }
        [Required]
        [Display(Name = "Wine Type")]
        public String Type { get; set; }
        public SelectList Types { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        public DateTime Year { get; set; }
        [Required]
        public String Producer { get; set; }
        public string Description { get; set; }
        [Required]
        public int GrapeVarietyId { get; set; }
        [Display(Name = "Grape Variety")]
        public SelectList GrapeVarieties { get; set; }
    }
}