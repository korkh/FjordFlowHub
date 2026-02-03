using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class FreightUpdatedConsumer(IMapper mapper) : IConsumer<FreightUpdated>
    {
        public async Task Consume(ConsumeContext<FreightUpdated> context)
        {
            Console.WriteLine("--------> Consuming freight updated: " + context.Message.Id);

            var item = mapper.Map<Item>(context.Message);

            // 1. Find & update MongoDB document by ID
            var result = await DB.Update<Item>()
                .Match(a => a.ID == context.Message.Id) //Find required cargo
                .ModifyOnly(
                    x => new
                    {
                        x.Description,
                        x.WeightKg,
                        x.LengthMeters,
                        x.HeightMeters,
                        x.PickupCity,
                        x.DeliveryCity,
                        // Seller & created At not updating
                    },
                    item
                ) //Transferring mapped object Item
                .ExecuteAsync();

            // 2. Check if updated
            if (!result.IsAcknowledged)
                throw new MessageException(typeof(FreightUpdated), "Problem updating mongoDb");
        }
    }
}
