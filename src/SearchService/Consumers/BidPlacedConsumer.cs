using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            Console.WriteLine("--> Consuming bid placed in SearchService");

            // Find the item in MongoDB
            var freight =
                await DB.Find<Item>().OneAsync(context.Message.FreightId)
                ?? throw new MessageException(
                    typeof(FreightFinished),
                    "Cannot retrieve this tender"
                );

            // Tender Logic: Update only if the bid is accepted and lower than the current one
            if (
                freight != null
                && (
                    freight.CurrentLowBid == null
                    || context.Message.BidStatus.Contains("Accepted")
                        && context.Message.Amount < freight.CurrentLowBid
                )
            )
            {
                freight.CurrentLowBid = context.Message.Amount;
                await freight.SaveAsync();
            }
        }
    }
}
