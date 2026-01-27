using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services
{
    public class FreightServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public FreightServiceHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<Item>> GetItemsForSearchDb()
        {
            // Get the last updated timestamp from MongoDB to fetch only new records
            var lastUpdated = await DB.Find<Item, string>()
                .Sort(x => x.Descending(x => x.UpdatedAt))
                .Project(x => x.UpdatedAt.ToString())
                .ExecuteFirstAsync();

            // Handle the case where the Search database is empty
            var dateParam = string.IsNullOrEmpty(lastUpdated) ? "" : lastUpdated;

            // Use string interpolation for cleaner URL construction
            var url = $"{_config["FreightServiceUrl"]}/api/freights?date={dateParam}";

            // Fetch items from FreightService. GetFromJsonAsync returns null if response is empty
            return await _httpClient.GetFromJsonAsync<List<Item>>(url) ?? new List<Item>();
        }
    }
}
