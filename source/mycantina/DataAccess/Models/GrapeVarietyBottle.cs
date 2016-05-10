namespace mycantina.DataAccess.Models
{
    public class GrapeVarietyBottle
    {
        public int GrapeVarietyId { get; set; }
        public int BottleId { get; set; }
        public Bottle Bottle { get; set; }
        public GrapeVariety GrapeVariety { get; set; }
    }
}