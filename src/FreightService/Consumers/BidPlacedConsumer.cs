using Contracts;
using FreightService.Data;
using MassTransit;

namespace FreightService.Consumers;

// Переименовали в Consumer, чтобы не путать с контрактом
public class BidPlacedConsumer(FreightDbContext dbContext) : IConsumer<BidPlaced>
{
    private readonly FreightDbContext _dbContext = dbContext;

    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--> Consuming bid placed");

        var freight = await _dbContext.Freights.FindAsync(context.Message.FreightId);

        // Update the current low bid
        if (
            freight.CurrentLowBid == null
            || context.Message.BidStatus.Contains("Accepted")
                && context.Message.Amount < freight.CurrentLowBid
        )
        {
            freight.CurrentLowBid = context.Message.Amount;
            await _dbContext.SaveChangesAsync();
        }
    }
}
