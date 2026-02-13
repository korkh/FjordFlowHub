using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        // 1. Initialize paged search
        var query = DB.PagedSearch<Item, Item>();

        // 2. Full-text search (requires text index in MongoDB)
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        // 3. Sorting logic
        query = searchParams.OrderBy switch
        {
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            "price" => query
                .Sort(x => x.Ascending(a => a.CurrentLowBid))
                .Sort(x => x.Ascending(a => a.AuctionEnd)),
            "description" => query.Sort(x => x.Ascending(a => a.Description)),
            "weight" => query.Sort(x => x.Descending(a => a.WeightKg)),
            // By default, sort by auction end time (soonest first)
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)),
        };

        // 4. Filtering logic
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            // Left only auctions that will end in the next 6 hours
            "endingSoon" => query.Match(x =>
                x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow
            ),
            "all" => query,
            // By default - Live, show only active auctions
            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow),
        };

        // 5. Seller/Winner filtering (Fix: added assignments to query variable)
        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query = query.Match(x => x.Seller == searchParams.Seller);
        }

        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query = query.Match(x => x.Winner == searchParams.Winner);
        }

        // 6. Pagination settings
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        // 7. Execute query
        var result = await query.ExecuteAsync();

        return Ok(
            new
            {
                results = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount,
            }
        );
    }
}
