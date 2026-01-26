using System.ComponentModel.DataAnnotations;

namespace FreightService.DTOs
{
    public class CreateFreightDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int WeightKg { get; set; }

        [Required]
        public double LengthMeters { get; set; }

        [Required]
        public double HeightMeters { get; set; }

        [Required]
        public string PickupCity { get; set; }

        [Required]
        public string DeliveryCity { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int ReservePrice { get; set; }

        [Required]
        public DateTime AuctionEnd { get; set; }
    }
}
