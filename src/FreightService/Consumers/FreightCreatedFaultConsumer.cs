using Contracts;
using MassTransit;

namespace FreightService.Consumers
{
    public class FreightCreatedFaultConsumer : IConsumer<Fault<FreightCreated>>
    {
        public async Task Consume(ConsumeContext<Fault<FreightCreated>> context)
        {
            System.Console.WriteLine("------> Consuming faulty creation");

            var exception = context.Message.Exceptions.First();

            if (exception.ExceptionType == "System.ArgumentException")
            {
                context.Message.Message.Description = "FooBar";
                await context.Publish(context.Message.Message);
            }
            else
            {
                Console.WriteLine("Not an argument exception - update error dashboard somewhere");
            }
        }
    }
}
