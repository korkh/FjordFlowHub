using MongoDB.Entities;

namespace SearchService.Models;

// Using MongoDB.Entities to inherit IEntity which provides an Id property
public class Item : Entity
{
    // Auction related properties
    public int ReservePrice { get; set; }
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; }
    public string Status { get; set; }

    // Flattened Cargo properties
    public string Description { get; set; }
    public int WeightKg { get; set; }
    public double LengthMeters { get; set; }
    public double HeightMeters { get; set; }
    public string PickupCity { get; set; }
    public string DeliveryCity { get; set; }
    public string ImageUrl { get; set; }
}
