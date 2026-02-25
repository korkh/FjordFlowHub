using MongoDB.Entities;

namespace BiddingService.Models
{
    public class Bid : Entity
    {
        public string FreightId { get; set; }
        public string Bidder { get; set; }
        public DateTime BidTime { get; set; } = DateTime.UtcNow;
        public int Amount { get; set; }
        public BidStatus BidStatus { get; set; }
    }
}
