namespace FreightService.DTOs
{
    public class UpdateFreightDto
    {
        public string Description { get; set; }
        public int WeightKg { get; set; }
        public double LengthMeters { get; set; }
        public double HeightMeters { get; set; }
        public string PickupCity { get; set; }
        public string DeliveryCity { get; set; }
    }
}