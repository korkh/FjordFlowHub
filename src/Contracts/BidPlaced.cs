namespace Contracts
{
    public class BidPlaced
    {
        public string Id { get; init; }
        public string FreightId { get; init; }
        public string Bidder { get; init; }
        public DateTime BidTime { get; init; }
        public int Amount { get; init; }
        public required string BidStatus { get; init; }
    }
}
