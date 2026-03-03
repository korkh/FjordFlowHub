using Contracts;
using FreightService.Data;
using MassTransit;

namespace FreightService.Consumers;

public class BidPlacedConsumer(FreightDbContext dbContext) : IConsumer<BidPlaced>
{
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--> Consuming bid placed");

        var freight = await dbContext.Freights.FindAsync(Guid.Parse(context.Message.FreightId));

        // Update the current low bid
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
            await dbContext.SaveChangesAsync();
        }
    }
}
