using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class FreightFinishedConsumer : IConsumer<FreightFinished>
    {
        public async Task Consume(ConsumeContext<FreightFinished> context)
        {
            Console.WriteLine("--> Consuming freight finished in SearchService");

            var freight = await DB.Find<Item>().OneAsync(context.Message.FreightId);

            if (freight == null)
                return;

            // If the tender was successful (sold), update winner and final amount
            if (context.Message.FreightSold)
            {
                freight.Winner = context.Message.Winner;
                freight.SoldAmount = context.Message.Amount ?? 0;
            }

            // Mark the status as finished for the UI to display correctly
            freight.Status = "Finished";

            await freight.SaveAsync();
        }
    }
}
