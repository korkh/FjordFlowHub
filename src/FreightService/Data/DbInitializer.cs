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
                // 1. Thermal insulation boards (Building materials)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 2500,
                    CurrentLowBid = 2500,
                    Seller = "Oslo Construction AS",
                    AuctionEnd = DateTime.UtcNow.AddDays(10),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Thermal insulation boards",
                        WeightKg = 1200,
                        LengthMeters = 2.4,
                        WidthMeters = 1.2,
                        HeightMeters = 2.0,
                        PickupCity = "Oslo",
                        DeliveryCity = "Bergen",
                        ImageUrl =
                            "https://media.sciencephoto.com/image/c0264488/800wm/C0264488-Thermal_insulation_boards.jpg",
                    },
                },
                // 2. Server rack equipment (IT)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 800,
                    CurrentLowBid = 750,
                    Seller = "Nordic Tech Electronics",
                    AuctionEnd = DateTime.UtcNow.AddHours(-10),
                    Status = Status.Finished,
                    Cargo = new Cargo
                    {
                        Description = "Server rack equipment",
                        WeightKg = 350,
                        LengthMeters = 0.8,
                        WidthMeters = 0.6,
                        HeightMeters = 1.8,
                        PickupCity = "Stavanger",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://robots.net/wp-content/uploads/2023/11/how-to-cool-a-server-rack-1701058282.jpg",
                    },
                },
                // 3. Handmade oak tables (Furniture)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 1500,
                    CurrentLowBid = 1400,
                    Seller = "Fjord Furniture",
                    AuctionEnd = DateTime.UtcNow.AddHours(-8),
                    Status = Status.ReserveNotMet,
                    Cargo = new Cargo
                    {
                        Description = "Handmade oak tables",
                        WeightKg = 850,
                        LengthMeters = 2.0,
                        WidthMeters = 1.0,
                        HeightMeters = 0.8,
                        PickupCity = "Trondheim",
                        DeliveryCity = "Drammen",
                        ImageUrl =
                            "https://tse1.mm.bing.net/th/id/OIP.njyS9YsiocSfh1Z0vTGtVgHaE8?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 4. Fresh King Crabs
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 5000,
                    CurrentLowBid = 5000,
                    Seller = "Tromsø Seafood Export",
                    AuctionEnd = DateTime.UtcNow.AddDays(33),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Fresh King Crabs (Shipment cancelled)",
                        WeightKg = 500,
                        LengthMeters = 1.2,
                        WidthMeters = 1.0,
                        HeightMeters = 1.0,
                        PickupCity = "Tromsø",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://tse2.mm.bing.net/th/id/OIP.OtcBpNov5y790IysA0L0RwHaJ4?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 5. Industrial Transformer (Heavy Load)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 12000,
                    CurrentLowBid = 11500,
                    Seller = "Norway Energy Solutions",
                    AuctionEnd = DateTime.UtcNow.AddDays(15),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Industrial Transformer",
                        WeightKg = 4500,
                        LengthMeters = 3.5,
                        WidthMeters = 2.2,
                        HeightMeters = 2.8,
                        PickupCity = "Kristiansand",
                        DeliveryCity = "Haugesund",
                        ImageUrl =
                            "https://scu-bucket-3.oss-eu-central-1.aliyuncs.com/uploads/2023/11/Ireland-energy-storage-container.jpg",
                    },
                },
                // 6. Winter tires set
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 500,
                    CurrentLowBid = 450,
                    Seller = "Oslo Auto Parts",
                    AuctionEnd = DateTime.UtcNow.AddHours(-12),
                    Status = Status.ReserveNotMet,
                    Cargo = new Cargo
                    {
                        Description = "Winter tires set (Peugeot 3008 compatible)",
                        WeightKg = 80,
                        LengthMeters = 0.7,
                        WidthMeters = 0.7,
                        HeightMeters = 1.0,
                        PickupCity = "Oslo",
                        DeliveryCity = "Lillehammer",
                        ImageUrl =
                            "https://tse1.mm.bing.net/th/id/OIP.NYDR-ovXtETkucCBRsH61wHaFj?cb=defcachec2&w=1500&h=1125&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 7. Archived documents (Palletized)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 200,
                    CurrentLowBid = 200,
                    Seller = "National Library",
                    AuctionEnd = DateTime.UtcNow.AddHours(23),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Archived documents and books",
                        WeightKg = 1500,
                        LengthMeters = 1.2,
                        WidthMeters = 0.8,
                        HeightMeters = 1.6,
                        PickupCity = "Mo i Rana",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://tse4.mm.bing.net/th/id/OIP.Ha3gZPJzVUlm1haTuP4a9QHaFk?cb=defcachec2&w=2000&h=1506&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 8. Solar panels
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 3000,
                    CurrentLowBid = 2800,
                    Seller = "SolarNordic AS",
                    AuctionEnd = DateTime.UtcNow.AddHours(-23),
                    Status = Status.Finished,
                    Cargo = new Cargo
                    {
                        Description = "Photovoltaic solar panels",
                        WeightKg = 900,
                        LengthMeters = 1.7,
                        WidthMeters = 1.1,
                        HeightMeters = 1.5,
                        PickupCity = "Fredrikstad",
                        DeliveryCity = "Alesund",
                        ImageUrl =
                            "https://cdn.shopify.com/s/files/1/0034/8913/6751/files/dc_watt_to_ac_watt_conversion_calculator_480x480.jpg?v=1670584657",
                    },
                },
                // 9. Green coffee beans
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 500,
                    CurrentLowBid = 500,
                    Seller = "Oslo Coffee Roasters",
                    AuctionEnd = DateTime.UtcNow.AddDays(15),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Green coffee beans in sacks",
                        WeightKg = 600,
                        LengthMeters = 1.2,
                        WidthMeters = 1.0,
                        HeightMeters = 1.2,
                        PickupCity = "Oslo",
                        DeliveryCity = "Skien",
                        ImageUrl =
                            "https://tse2.mm.bing.net/th/id/OIP.Zb0Y6BLPz6QsW7ISGW8g_wHaE9?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 10. CNC Milling Machine
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 15000,
                    CurrentLowBid = 15000,
                    Seller = "Fjord Industrial",
                    AuctionEnd = DateTime.UtcNow.AddDays(40),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "CNC Milling Machine",
                        WeightKg = 2500,
                        LengthMeters = 2.0,
                        WidthMeters = 1.8,
                        HeightMeters = 2.2,
                        PickupCity = "Sandnes",
                        DeliveryCity = "Bodø",
                        ImageUrl =
                            "https://tse4.mm.bing.net/th/id/OIP.mIE2A2DeLOrCdM4BXiCXBQHaHa?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
            };

            context.AddRange(freights);
            context.SaveChanges();
            Console.WriteLine(
                "---> Database seeded with 10 FjordFlow samples using Tender logic and Cargo dimensions"
            );
        }
    }
}
