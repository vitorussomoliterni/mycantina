using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models
{
    class User
    {
        public User()
        {
            Reviews = new List<Review>();
            User_Bottles = new List<User_Bottle>();
        }
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String MiddleNames { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public List<User_Bottle> User_Bottles { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
