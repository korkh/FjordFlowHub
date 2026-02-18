using MongoDB.Entities;

namespace SearchService.Models;

// Using MongoDB.Entities to inherit Entity which provides a String ID for MongoDB
public class Item : Entity
{
    // Auction related properties
    public int ReservePrice { get; set; }
    public required string Seller { get; set; }
    public string Winner { get; set; } // Nullable as there might be no winner yet
    public int? SoldAmount { get; set; }
    public int? CurrentLowBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; }
    public required string Status { get; set; }

    // Flattened Cargo properties
    public required string Description { get; set; }
    public int WeightKg { get; set; }

    // Dimensions for logistics
    public double LengthMeters { get; set; }
    public double WidthMeters { get; set; }
    public double HeightMeters { get; set; }
    public double VolumeM3 { get; set; }

    public required string PickupCity { get; set; }
    public required string DeliveryCity { get; set; }
    public required string ImageUrl { get; set; }
}
