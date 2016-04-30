using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.User
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Display(Name = "Middle Names")]
        public String MiddleNames { get; set; }
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        [Display(Name = "Email Address")]
        public String Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}