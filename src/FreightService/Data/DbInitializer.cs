using FreightService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreightService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            SeedData(scope.ServiceProvider.GetRequiredService<FreightDbContext>());
        }

        private static void SeedData(FreightDbContext context)
        {
            context.Database.Migrate();

            if (context.Freights.Any())
            {
                Console.WriteLine("---> Database already has data - skipping seed");
                return;
            }

            var freights = new List<Freight>
            {
                // 1. LIVE - No bids yet
                new Freight
                {
                    Id = Guid.Parse("afbee524-5972-4075-8af4-519f95859505"),
                    ReservePrice = 2500,
                    CurrentHighBid = 2500, // Starting point for tender
                    Seller = "Oslo Construction AS",
                    AuctionEnd = DateTime.UtcNow.AddDays(10),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Thermal insulation boards",
                        WeightKg = 1200,
                        LengthMeters = 4.5,
                        HeightMeters = 2.1,
                        PickupCity = "Oslo",
                        DeliveryCity = "Bergen",
                        ImageUrl =
                            "https://media.sciencephoto.com/image/c0264488/800wm/C0264488-Thermal_insulation_boards.jpg",
                    },
                },
                // 2. RESERVED - Some carrier offered 750
                new Freight
                {
                    Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1d9cc2235a0c"),
                    ReservePrice = 800,
                    CurrentHighBid = 750,
                    Seller = "Nordic Tech Electronics",
                    AuctionEnd = DateTime.UtcNow.AddDays(4),
                    Status = Status.Reserved,
                    Cargo = new Cargo
                    {
                        Description = "Server rack equipment",
                        WeightKg = 350,
                        LengthMeters = 1.2,
                        HeightMeters = 0.8,
                        PickupCity = "Stavanger",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://robots.net/wp-content/uploads/2023/11/how-to-cool-a-server-rack-1701058282.jpg",
                    },
                },
                // 3. DELIVERED - Winner offered 1400 (below budget of 1500)
                new Freight
                {
                    Id = Guid.Parse("bbd57683-939e-4360-a15d-99f722055660"),
                    ReservePrice = 1500,
                    Winner = "ExpressLogistics",
                    SoldAmount = 1400,
                    CurrentHighBid = 1400,
                    Seller = "Fjord Furniture",
                    AuctionEnd = DateTime.UtcNow.AddDays(-1),
                    Status = Status.Delivered,
                    Cargo = new Cargo
                    {
                        Description = "Handmade oak tables",
                        WeightKg = 850,
                        LengthMeters = 3.0,
                        HeightMeters = 1.5,
                        PickupCity = "Trondheim",
                        DeliveryCity = "Drammen",
                        ImageUrl =
                            "https://tse1.mm.bing.net/th/id/OIP.njyS9YsiocSfh1Z0vTGtVgHaE8?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 4. CANCELLED
                new Freight
                {
                    Id = Guid.Parse("dc1e4071-d19d-459b-b840-385a30351234"),
                    ReservePrice = 5000,
                    CurrentHighBid = 5000,
                    Seller = "Tromsø Seafood Export",
                    AuctionEnd = DateTime.UtcNow.AddDays(-2),
                    Status = Status.Cancelled,
                    Cargo = new Cargo
                    {
                        Description = "Fresh King Crabs (Shipment cancelled)",
                        WeightKg = 500,
                        LengthMeters = 2.0,
                        HeightMeters = 1.0,
                        PickupCity = "Tromsø",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://tse2.mm.bing.net/th/id/OIP.OtcBpNov5y790IysA0L0RwHaJ4?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 5. LIVE - Active competition, bid is already lower than budget
                new Freight
                {
                    Id = Guid.Parse("6a5011a1-910a-4bab-9817-3f7fa41c6014"),
                    ReservePrice = 12000,
                    CurrentHighBid = 11500,
                    Seller = "Norway Energy Solutions equipment",
                    AuctionEnd = DateTime.UtcNow.AddDays(15),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Industrial Transformer",
                        WeightKg = 4500,
                        LengthMeters = 3.5,
                        HeightMeters = 2.8,
                        PickupCity = "Kristiansand",
                        DeliveryCity = "Haugesund",
                        ImageUrl =
                            "https://scu-bucket-3.oss-eu-central-1.aliyuncs.com/uploads/2023/11/Ireland-energy-storage-container.jpg",
                    },
                },
                // 6. DELIVERED
                new Freight
                {
                    Id = Guid.Parse("40490d57-5d76-4835-bc46-7011a3060f94"),
                    ReservePrice = 500,
                    Winner = "FastDelivery",
                    SoldAmount = 450,
                    CurrentHighBid = 450,
                    Seller = "Oslo Auto Parts",
                    AuctionEnd = DateTime.UtcNow.AddDays(-5),
                    Status = Status.Delivered,
                    Cargo = new Cargo
                    {
                        Description = "Winter tires set (Peugeot 3008 compatible)",
                        WeightKg = 80,
                        LengthMeters = 1.0,
                        HeightMeters = 1.0,
                        PickupCity = "Oslo",
                        DeliveryCity = "Lillehammer",
                        ImageUrl =
                            "https://tse1.mm.bing.net/th/id/OIP.NYDR-ovXtETkucCBRsH61wHaFj?cb=defcachec2&w=1500&h=1125&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 7. LIVE
                new Freight
                {
                    Id = Guid.Parse("3659da20-94f4-4a7b-a487-3d961e682e70"),
                    ReservePrice = 200,
                    CurrentHighBid = 200,
                    Seller = "National Library",
                    AuctionEnd = DateTime.UtcNow.AddHours(4),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Archived documents and books",
                        WeightKg = 1500,
                        LengthMeters = 5.0,
                        HeightMeters = 2.0,
                        PickupCity = "Mo i Rana",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://tse4.mm.bing.net/th/id/OIP.Ha3gZPJzVUlm1haTuP4a9QHaFk?cb=defcachec2&w=2000&h=1506&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 8. RESERVED
                new Freight
                {
                    Id = Guid.Parse("6a5011a1-910a-4bab-9817-3f7fa41c6015"),
                    ReservePrice = 3000,
                    CurrentHighBid = 2800,
                    Seller = "SolarNordic AS",
                    AuctionEnd = DateTime.UtcNow.AddDays(7),
                    Status = Status.Reserved,
                    Cargo = new Cargo
                    {
                        Description = "Photovoltaic solar panels",
                        WeightKg = 900,
                        LengthMeters = 2.5,
                        HeightMeters = 1.5,
                        PickupCity = "Fredrikstad",
                        DeliveryCity = "Alesund",
                        ImageUrl =
                            "https://cdn.shopify.com/s/files/1/0034/8913/6751/files/dc_watt_to_ac_watt_conversion_calculator_480x480.jpg?v=1670584657",
                    },
                },
                // 9. LIVE
                new Freight
                {
                    Id = Guid.Parse("afbee524-5972-4075-8af4-519f95859510"),
                    ReservePrice = 500,
                    CurrentHighBid = 500,
                    Seller = "Oslo Coffee Roasters",
                    AuctionEnd = DateTime.UtcNow.AddDays(3),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Green coffee beans in sacks",
                        WeightKg = 600,
                        LengthMeters = 1.5,
                        HeightMeters = 1.2,
                        PickupCity = "Oslo",
                        DeliveryCity = "Skien",
                        ImageUrl =
                            "https://tse2.mm.bing.net/th/id/OIP.Zb0Y6BLPz6QsW7ISGW8g_wHaE9?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 10. CANCELLED
                new Freight
                {
                    Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1d9cc2235a11"),
                    ReservePrice = 15000,
                    CurrentHighBid = 15000,
                    Seller = "Fjord Industrial",
                    Status = Status.Cancelled,
                    AuctionEnd = DateTime.UtcNow.AddDays(-10),
                    Cargo = new Cargo
                    {
                        Description = "CNC Milling Machine (Returned to seller)",
                        WeightKg = 2500,
                        LengthMeters = 2.0,
                        HeightMeters = 2.5,
                        PickupCity = "Sandnes",
                        DeliveryCity = "Bodø",
                        ImageUrl =
                            "https://tse4.mm.bing.net/th/id/OIP.mIE2A2DeLOrCdM4BXiCXBQHaHa?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
            };

            context.AddRange(freights);
            context.SaveChanges();
            Console.WriteLine("---> Database seeded with 10 FjordFlow samples using Tender logic");
        }
    }
}
