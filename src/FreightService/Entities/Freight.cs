namespace FreightService.Entities
{
    public class Freight
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; } = 0; // Minimum acceptable price
        public string Seller { get; set; } // Shipper
        public string Winner { get; set; } // Carrier who won the auction
        public int SoldAmount { get; set; }
        public int CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AuctionEnd { get; set; } // Time when the auction ends
        public Status Status { get; set; }

        // Navigation property for related Cargo
        public Cargo Cargo { get; set; } = null!;
    }
}
