using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using mycantina.DataAccess.Models;

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
        public MultiSelectList Countries { get; set; }
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
        [Display(Name = "Grape Variety")]
        public int[] Varieties { get; set; }
        public MultiSelectList GrapeVarieties { get; set; }
    }
}