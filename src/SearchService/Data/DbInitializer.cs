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
            await DB.InitAsync(
                "SearchServiceDB",
                MongoClientSettings.FromConnectionString(
                    app.Configuration.GetConnectionString("MongoDbConnection")!
                )
            );

            // Create text index on Item collection
            // Not elasticsearch, but a simple text index in MongoDB
            await DB.Index<Item>()
                .Key(x => x.Description, KeyType.Text)
                .Key(x => x.PickupCity, KeyType.Text)
                .Key(x => x.DeliveryCity, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();

            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<FreightServiceHttpClient>();

            var items = await httpClient.GetItemsForSearchDb();

            Console.WriteLine(items.Count + "returned from the freight service");

            if (items.Count > 0)
            {
                await DB.SaveAsync(items);
            }
        }
    }
}
