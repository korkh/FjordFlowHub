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
                // 1. LIVE
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
                        PickupCity = "Oslo",
                        DeliveryCity = "Bergen",
                        ImageUrl =
                            "https://media.sciencephoto.com/image/c0264488/800wm/C0264488-Thermal_insulation_boards.jpg",
                    },
                },
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
                        PickupCity = "Stavanger",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://robots.net/wp-content/uploads/2023/11/how-to-cool-a-server-rack-1701058282.jpg",
                    },
                },
                // 3. ReserveNotMet
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
                        PickupCity = "Trondheim",
                        DeliveryCity = "Drammen",
                        ImageUrl =
                            "https://tse1.mm.bing.net/th/id/OIP.njyS9YsiocSfh1Z0vTGtVgHaE8?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
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
                        PickupCity = "Tromsø",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://tse2.mm.bing.net/th/id/OIP.OtcBpNov5y790IysA0L0RwHaJ4?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 5. LIVE
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
                        PickupCity = "Kristiansand",
                        DeliveryCity = "Haugesund",
                        ImageUrl =
                            "https://scu-bucket-3.oss-eu-central-1.aliyuncs.com/uploads/2023/11/Ireland-energy-storage-container.jpg",
                    },
                },
                // 6. ReserveNotMet
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
                        PickupCity = "Oslo",
                        DeliveryCity = "Lillehammer",
                        ImageUrl =
                            "https://tse1.mm.bing.net/th/id/OIP.NYDR-ovXtETkucCBRsH61wHaFj?cb=defcachec2&w=1500&h=1125&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
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
                        PickupCity = "Mo i Rana",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://tse4.mm.bing.net/th/id/OIP.Ha3gZPJzVUlm1haTuP4a9QHaFk?cb=defcachec2&w=2000&h=1506&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 8. FINISHED (2/3)
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
                        PickupCity = "Fredrikstad",
                        DeliveryCity = "Alesund",
                        ImageUrl =
                            "https://cdn.shopify.com/s/files/1/0034/8913/6751/files/dc_watt_to_ac_watt_conversion_calculator_480x480.jpg?v=1670584657",
                    },
                },
                // 9. LIVE
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
                        PickupCity = "Oslo",
                        DeliveryCity = "Skien",
                        ImageUrl =
                            "https://tse2.mm.bing.net/th/id/OIP.Zb0Y6BLPz6QsW7ISGW8g_wHaE9?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
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
                        PickupCity = "Sandnes",
                        DeliveryCity = "Bodø",
                        ImageUrl =
                            "https://tse4.mm.bing.net/th/id/OIP.mIE2A2DeLOrCdM4BXiCXBQHaHa?cb=defcachec2&rs=1&pid=ImgDetMain&o=7&rm=3",
                    },
                },
                // 11. FINISHED (3/3)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 1000,
                    CurrentLowBid = 900,
                    Seller = "Lumber King",
                    AuctionEnd = DateTime.UtcNow.AddHours(-3),
                    Status = Status.Finished,
                    Cargo = new Cargo
                    {
                        Description = "Raw timber logs",
                        WeightKg = 3000,
                        PickupCity = "Hamar",
                        DeliveryCity = "Larvik",
                        ImageUrl =
                            "https://cdn.pixabay.com/photo/2016/11/29/07/29/wood-1868104_1280.jpg",
                    },
                },
                // 12. ENDING SOON (2/3)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 400,
                    CurrentLowBid = 400,
                    Seller = "QuickMove",
                    AuctionEnd = DateTime.UtcNow.AddDays(60),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Office chairs and desks",
                        WeightKg = 400,
                        PickupCity = "Oslo",
                        DeliveryCity = "Asker",
                        ImageUrl =
                            "https://cdn.pixabay.com/photo/2017/03/28/12/11/chairs-2181960_1280.jpg",
                    },
                },
                // 13. ENDING SOON (3/3)
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 2200,
                    CurrentLowBid = 2100,
                    Seller = "BioFuel AS",
                    AuctionEnd = DateTime.UtcNow.AddHours(24),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Recycled plastic pellets",
                        WeightKg = 2000,
                        PickupCity = "Moss",
                        DeliveryCity = "Porsgrunn",
                        ImageUrl =
                            "https://cdn.pixabay.com/photo/2017/10/03/22/42/granules-2814498_1280.jpg",
                    },
                },
                // 14. LIVE
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 6000,
                    CurrentLowBid = 5800,
                    Seller = "Arctic Fish",
                    AuctionEnd = DateTime.UtcNow.AddDays(25),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Frozen Salmon pallets",
                        WeightKg = 1500,
                        PickupCity = "Ålesund",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://cdn.pixabay.com/photo/2015/09/05/21/10/frozen-925362_1280.jpg",
                    },
                },
                // 15. LIVE
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 300,
                    CurrentLowBid = 300,
                    Seller = "Private Homeowner",
                    AuctionEnd = DateTime.UtcNow.AddDays(20),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Old Piano (transport only)",
                        WeightKg = 250,
                        PickupCity = "Bergen",
                        DeliveryCity = "Voss",
                        ImageUrl =
                            "https://cdn.pixabay.com/photo/2016/08/17/17/38/piano-1601094_1280.jpg",
                    },
                },
                new Freight
                {
                    Id = Guid.NewGuid(),
                    ReservePrice = 900,
                    CurrentLowBid = 900,
                    Seller = "Event Rentals",
                    AuctionEnd = DateTime.UtcNow.AddDays(11),
                    Status = Status.Live,
                    Cargo = new Cargo
                    {
                        Description = "Party Tents (Event postponed)",
                        WeightKg = 400,
                        PickupCity = "Ski",
                        DeliveryCity = "Oslo",
                        ImageUrl =
                            "https://cdn.pixabay.com/photo/2018/08/20/00/01/party-3617875_1280.jpg",
                    },
                },
            };

            context.AddRange(freights);
            context.SaveChanges();
            Console.WriteLine("---> Database seeded with 10 FjordFlow samples using Tender logic");
        }
    }
}
