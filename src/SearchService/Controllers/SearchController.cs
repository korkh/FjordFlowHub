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
        // 1. Start building the query
        var query = DB.PagedSearch<Item, Item>();

        // 2. Full-text search logic
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }
        else
        {
            // Default sorting if no search term is provided
            query.Sort(x => x.Ascending(a => a.AuctionEnd));
        }

        // 3. Sorting logic based on params
        query = searchParams.OrderBy switch
        {
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            // For a tender, the lowest price is the most attractive
            "price" => query.Sort(x => x.Ascending(a => a.CurrentHighBid)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)),
        };

        // 4. Filtering logic (Live, Finished, etc.)
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x =>
                x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow
            ),
            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow), // Live by default
        };

        // 5. Seller/Winner filtering
        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }

        // 6. Pagination
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync();

        // Wrap result into a standard response with total count
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
