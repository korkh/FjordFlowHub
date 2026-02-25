using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers
{
    public class FreightFinishedConsumer : IConsumer<FreightFinished>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public FreightFinishedConsumer(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<FreightFinished> context)
        {
            System.Console.WriteLine(
                "------> Freight finished message received by Notification Service"
            );

            await _hubContext.Clients.All.SendAsync("FreightFinished", context.Message);
        }
    }
}
