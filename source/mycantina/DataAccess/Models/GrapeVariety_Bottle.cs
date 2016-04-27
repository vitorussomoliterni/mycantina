namespace mycantina.DataAccess.Models
{
    public class GrapeVariety_Bottle
    {
        public int GrapeVarietyId { get; set; }
        public int BottleId { get; set; }
        public Bottle Bottle { get; set; }
        public GrapeVariety GrapeVariety { get; set; }
    }
}