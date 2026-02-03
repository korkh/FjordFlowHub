namespace Contracts
{
    // Using record for immutability and built-in serialization support
    public record FreightUpdated
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int ReservePrice { get; set; }
        public int WeightKg { get; set; }
        public double LengthMeters { get; set; }
        public double HeightMeters { get; set; }
        public string PickupCity { get; set; }
        public string DeliveryCity { get; set; }

        public string ImageUrl { get; set; }
        public string Status { get; set; }
    }
}
