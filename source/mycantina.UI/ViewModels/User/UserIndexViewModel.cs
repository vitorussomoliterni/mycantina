using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mycantina.UI.ViewModels.User
{
    public class UserIndexViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public String FullName { get; set; }
        [Display(Name = "Email address")]
        public String Email { get; set; }
    }
}