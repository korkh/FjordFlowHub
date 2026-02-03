using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

// Standard .NET 10 primary constructor
public class FreightCreatedConsumer(IMapper mapper) : IConsumer<FreightCreated>
{
    public async Task Consume(ConsumeContext<FreightCreated> context)
    {
        Console.WriteLine("---> Consuming freight created: " + context.Message.Id);

        // Map the contract message to our Search MongoDB Item entity
        var item = mapper.Map<Item>(context.Message);

        //handling faults in consumer
        if (item.Description == "Foo")
            throw new ArgumentException("Cannot make a freight with nam Foo");

        // Save the item to MongoDB.
        // In MongoDB.Entities, SaveAsync performs an 'upsert' automatically.
        await item.SaveAsync();
    }
}
