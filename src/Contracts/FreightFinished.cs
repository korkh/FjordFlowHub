namespace Contracts
{
    // Using record for immutability and easy serialization
    public record FreightFinished
    {
        public string FreightId { get; init; }
        public bool FreightSold { get; init; } // Quick check for success
        public string Seller { get; init; }
        public string Winner { get; init; } // Null if not sold
        public int? Amount { get; init; } // Null if no bids were made
    }
}
