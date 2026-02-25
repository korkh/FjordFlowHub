using Contracts;
using FreightService.Data;
using FreightService.Entities;
using MassTransit;

namespace FreightService.Consumers
{
    public class FreightFinishedConsumer(FreightDbContext dbContext) : IConsumer<FreightFinished>
    {
        private readonly FreightDbContext _dbContext = dbContext;

        public async Task Consume(ConsumeContext<FreightFinished> context)
        {
            Console.WriteLine("--> Consuming freight finished");

            var freight = await _dbContext.Freights.FindAsync(
                Guid.Parse(context.Message.FreightId)
            );
            //if freight sold
            if (context.Message.FreightSold)
            {
                freight.Winner = context.Message.Winner;
                freight.SoldAmount = context.Message.Amount;
            }

            //Tender
            freight.Status =
                (freight.SoldAmount > 0 && freight.SoldAmount <= freight.ReservePrice)
                    ? Status.Finished
                    : Status.ReserveNotMet;

            await _dbContext.SaveChangesAsync();
        }
    }
}
