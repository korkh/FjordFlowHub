using System.ComponentModel.DataAnnotations.Schema;

namespace FreightService.Entities
{
    [Table("Cargos")]
    public class Cargo
    {
        public Guid Id { get; set; }

        // Cargo details
        public string Description { get; set; } = null!; // Description of the cargo
        public int WeightKg { get; set; } // Weight in kilograms
        public double LengthMeters { get; set; }
        public double HeightMeters { get; set; }

        // Location of pickup and delivery
        public string PickupCity { get; set; } = null!; //For example: "Oslo"
        public string DeliveryCity { get; set; } = null!; // For example: "Bergen"
        public required string ImageUrl { get; set; }

        //Navigation property for related Freight
        public Freight Freight { get; set; } = null!;
        public Guid FreightId { get; set; }
    }
}
