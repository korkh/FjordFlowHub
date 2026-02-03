using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            // Initialize MongoDB connection
            await DB.InitAsync(
                "SearchServiceDB",
                MongoClientSettings.FromConnectionString(
                    app.Configuration.GetConnectionString("MongoDbConnection")!
                )
            );

            // Create text index for full-text search capabilities
            // This allows searching by description and route (Pickup/Delivery cities)
            await DB.Index<Item>()
                .Key(x => x.Description, KeyType.Text)
                .Key(x => x.PickupCity, KeyType.Text)
                .Key(x => x.DeliveryCity, KeyType.Text)
                .CreateAsync();

            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<FreightServiceHttpClient>();

            try
            {
                // Fetch only new or updated items from the main FreightService
                var items = await httpClient.GetItemsForSearchDb();

                Console.WriteLine($"---> {items.Count} items returned from the freight service");

                if (items.Count > 0)
                {
                    // SaveAsync in MongoDB.Entities works as an 'upsert'
                    // It will update existing records by ID or insert new ones
                    await DB.SaveAsync(items);
                    Console.WriteLine("---> Successfully synced data to MongoDB");
                }
            }
            catch (Exception ex)
            {
                // Log the error but allow the service to start
                // It can try to sync again later via a background task or manual trigger
                Console.WriteLine($"---> Cannot sync data from FreightService: {ex.Message}");
            }
        }
    }
}
