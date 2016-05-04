using System;

namespace mycantina.DataAccess.Models
{
    public class User_Bottle
    {
        public int Id { get; set; }
        public int WineFormatId { get; set; }
        public DateTime? DateAcquired { get; set; }
        public DateTime? DateOpened { get; set; }
        public int QtyOwned { get; set; }
        public Boolean Owned { get; set; }
        public decimal PricePaid { get; set; }
        public Bottle Bottle { get; set; }
        public User User { get; set; }
        public WineFormat WineFormat { get; set; }
    }
}