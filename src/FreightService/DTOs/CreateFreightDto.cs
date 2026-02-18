using System.ComponentModel.DataAnnotations;

namespace FreightService.DTOs
{
    public class CreateFreightDto
    {
        [Required]
        public required string Description { get; set; }

        [Required]
        [Range(1, 50000)] // Standard truck limit
        public int WeightKg { get; set; }

        [Required]
        public double LengthMeters { get; set; }

        [Required]
        public double WidthMeters { get; set; }

        [Required]
        public double HeightMeters { get; set; }

        [Required]
        public required string PickupCity { get; set; }

        [Required]
        public required string DeliveryCity { get; set; }

        [Required]
        public required string ImageUrl { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ReservePrice { get; set; }

        [Required]
        public DateTime AuctionEnd { get; set; }
    }
}
