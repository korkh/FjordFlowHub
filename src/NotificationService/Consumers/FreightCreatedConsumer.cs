using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers
{
    public class FreightCreatedConsumer : IConsumer<FreightCreated>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        //SignalR
        public FreightCreatedConsumer(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<FreightCreated> context)
        {
            Console.WriteLine("------> Freight created message received by Notification Service");
            await _hubContext.Clients.All.SendAsync("FreightCreated", context.Message);
        }
    }
}
