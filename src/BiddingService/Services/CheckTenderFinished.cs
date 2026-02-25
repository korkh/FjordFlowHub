using BiddingService.Models;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace BiddingService.Services
{
    /*
    * Background service that periodically checks for tenders whose end time has passed
     * and processes them as finished.
     */
    public class CheckTenderFinished(ILogger<CheckTenderFinished> logger, IServiceProvider services)
        : BackgroundService
    {
        private readonly ILogger<CheckTenderFinished> _logger = logger;
        private readonly IServiceProvider _services = services;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "CheckTenderFinished service starting check for finished tenders."
            );

            // Register a callback to log when the service is stopping (e.g., app shutdown)
            stoppingToken.Register(() =>
                _logger.LogInformation("===> CheckTenderFinished service is stopping.")
            );

            while (!stoppingToken.IsCancellationRequested)
            {
                // Core logic to process expired tenders
                await CheckForFinishedTenders(stoppingToken);

                // Wait for 10 seconds before the next iteration
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task CheckForFinishedTenders(CancellationToken stoppingToken)
        {
            // Fetch tenders that have expired but haven't been processed yet (Finished == false)
            var finishedTenders = await DB.Find<Freight>()
                .Match(a => a.AuctionEnd < DateTime.UtcNow)
                .Match(x => !x.Finished)
                .ExecuteAsync(stoppingToken);

            if (finishedTenders.Count == 0)
                return;

            _logger.LogInformation("===> Found {count} completed tenders.", finishedTenders.Count);

            // Use IServiceProvider to create a scope for resolving scoped services like IPublishEndpoint
            using var scope = _services.CreateScope();
            var tenderService = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            foreach (var tender in finishedTenders)
            {
                // Mark tender as finished in the database to prevent duplicate processing
                tender.Finished = true;
                await tender.SaveAsync(null, stoppingToken);

                // Find the winning bid (lowest accepted bid for reverse auction/tender)
                var winningBid = await DB.Find<Bid>()
                    .Match(x => x.FreightId == tender.ID)
                    .Match(x => x.BidStatus == BidStatus.Accepted)
                    .Sort(x => x.Ascending(b => b.Amount))
                    .ExecuteFirstAsync(stoppingToken);

                /*
                 * Publish a FreightFinished event to RabbitMQ.
                 * Other services (FreightService, SearchService) will consume this to update status.
                 */
                await tenderService.Publish(
                    new FreightFinished
                    {
                        FreightSold = winningBid != null,
                        FreightId = tender.ID,
                        Winner = winningBid?.Bidder,
                        Amount = winningBid?.Amount,
                        Seller = tender.Seller,
                    },
                    stoppingToken
                );
            }
        }
    }
}
