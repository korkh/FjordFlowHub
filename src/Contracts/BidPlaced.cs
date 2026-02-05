namespace Contracts
{
    public class BidPlaced
    {
        public required string Id { get; init; } // ID of the bid record
        public required string FreightId { get; init; } // ID of the freight being bid on
        public required string Bidder { get; init; } // Username of the person who placed the bid
        public DateTime BidTime { get; init; }
        public int Amount { get; init; }
        public required string BidStatus { get; init; } // e.g., "Accepted", "TooLow", "Finished"}
    }
}
