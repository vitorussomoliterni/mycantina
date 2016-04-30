using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.User
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Display(Name = "Middle Names")]
        public String MiddleNames { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}