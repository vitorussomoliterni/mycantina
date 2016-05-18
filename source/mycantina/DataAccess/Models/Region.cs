using System.Collections.Generic;

namespace mycantina.DataAccess.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public List<Bottle> Bottles { get; set; }
    }
}