namespace Contracts
{
    // Using record for immutability and built-in serialization support
    public record FreightUpdated
    {
        public string Id { get; set; } // Matches MongoDB Entity ID format
        public required string Description { get; set; }
        public int ReservePrice { get; set; }
        public int WeightKg { get; set; }
        public double LengthMeters { get; set; }
        public double WidthMeters { get; set; }
        public double HeightMeters { get; set; }
        public double VolumeM3 { get; set; } // Added volume for search sorting consistency

        public required string PickupCity { get; set; }
        public required string DeliveryCity { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
    }
}
