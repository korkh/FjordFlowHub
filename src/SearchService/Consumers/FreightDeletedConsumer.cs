using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class FreightDeletedConsumer : IConsumer<FreightDeleted>
    {
        public async Task Consume(ConsumeContext<FreightDeleted> context)
        {
            Console.WriteLine("--------> Consuming freight deleted: " + context.Message.Id);

            var result = await DB.DeleteAsync<Item>(context.Message.Id);

            if (!result.IsAcknowledged || result.DeletedCount == 0)
            {
                // If no object in search, it can be duplicate or fail in queue.
                // Throw exception for MassTransit retry
                throw new MessageException(
                    typeof(FreightDeleted),
                    "Problem deleting from mongodb or item not found"
                );
            }
        }
    }
}
