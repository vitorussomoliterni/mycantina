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
        public int RegionId { get; set; }
        public SelectList Regions { get; set; }
        [Required]
        public int CountryId { get; set; }
        public SelectList Countries { get; set; }
        [Required]
        [Display(Name = "Wine Type")]
        public int WineTypeId { get; set; }
        public SelectList WineTypes { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public String Producer { get; set; }
        public string Description { get; set; }
        [Required]
        public int GrapeVarietyId { get; set; }
        [Display(Name = "Grape Variety")]
        public SelectList GrapeVarieties { get; set; }
    }
}