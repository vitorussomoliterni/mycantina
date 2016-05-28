using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models
{
    public class Consumer
    {
        public Consumer()
        {
            Reviews = new List<Review>();
            ConsumerBottles = new List<ConsumerBottle>();
        }
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String MiddleNames { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public List<ConsumerBottle> ConsumerBottles { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
