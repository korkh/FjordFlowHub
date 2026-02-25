namespace Contracts
{
    // Using record for immutability and easy serialization
    public record FreightFinished
    {
        public string FreightId { get; init; }
        public bool FreightSold { get; init; }
        public string Seller { get; init; }
        public string Winner { get; init; }
        public int? Amount { get; init; }
    }
}
