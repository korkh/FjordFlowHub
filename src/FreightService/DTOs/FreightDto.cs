namespace FreightService.DTOs
{
    public class FreightDto
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; }
        public required string Seller { get; set; }
        public string Winner { get; set; }
        public int SoldAmount { get; set; }
        public int CurrentLowBid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime AuctionEnd { get; set; }
        public required string Status { get; set; }

        // Flattened properties from Cargo
        public required string Description { get; set; }
        public int WeightKg { get; set; }
        public double LengthMeters { get; set; }
        public double WidthMeters { get; set; }
        public double HeightMeters { get; set; }
        public double VolumeM3 { get; set; }

        public required string PickupCity { get; set; }
        public required string DeliveryCity { get; set; }
        public required string ImageUrl { get; set; }
    }
}
