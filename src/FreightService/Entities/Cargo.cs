using System.ComponentModel.DataAnnotations.Schema;

namespace FreightService.Entities
{
    [Table("Cargos")]
    public class Cargo
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public int WeightKg { get; set; }

        // Dimensions
        public double LengthMeters { get; set; }
        public double WidthMeters { get; set; }
        public double HeightMeters { get; set; }

        // Calculated Volume
        public double VolumeM3 => Math.Round(LengthMeters * WidthMeters * HeightMeters, 2);

        public string PickupCity { get; set; } = null!;
        public string DeliveryCity { get; set; } = null!;
        public required string ImageUrl { get; set; }

        public Freight Freight { get; set; } = null!;
        public Guid FreightId { get; set; }
    }
}
