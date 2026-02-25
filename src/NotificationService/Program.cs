using MassTransit;
using NotificationService.Consumers;
using NotificationService.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    //Handle faulty freifgt creation
    x.AddConsumersFromNamespaceContaining<FreightCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("notification", false));

    x.UsingRabbitMq(
        (context, cnf) =>
        {
            cnf.Host(
                builder.Configuration["RabbitMq:Host"],
                "/",
                h =>
                {
                    h.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                    h.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
                }
            );
            cnf.ConfigureEndpoints(context);
        }
    );
});

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<NotificationHub>("/notifications");

app.Run();
