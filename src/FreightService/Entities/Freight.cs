namespace FreightService.Entities;

public class Freight
{
    public Guid Id { get; set; }

    // In a tender, this is the MAXIMUM price the Shipper is willing to pay
    public int ReservePrice { get; set; } = 0;

    public string Seller { get; set; } // Shipper (who needs delivery)

    // Carrier who offered the lowest price
    public string Winner { get; set; }

    public int SoldAmount { get; set; }

    // Current highest bid from a Carrier
    public int CurrentHighBid { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime AuctionEnd { get; set; }
    public Status Status { get; set; }

    public Cargo Cargo { get; set; } = null!;
}
