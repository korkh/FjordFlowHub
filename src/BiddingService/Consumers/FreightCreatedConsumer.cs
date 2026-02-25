using BiddingService.Models;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace BiddingService.Consumers
{
    public class FreightCreatedConsumer : IConsumer<FreightCreated>
    {
        public async Task Consume(ConsumeContext<FreightCreated> context)
        {
            var freight = new Freight
            {
                ID = context.Message.Id.ToString(),
                Seller = context.Message.Seller,
                AuctionEnd = context.Message.AuctionEnd,
                ReservePrice = context.Message.ReservePrice,
            };

            await freight.SaveAsync();
            if (freight == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
