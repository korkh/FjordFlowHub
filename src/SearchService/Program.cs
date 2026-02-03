using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfiles).Assembly);

// Register the HTTP client with a resilience policy (Polly)
// This will retry connecting to the FreightService if it's temporarily unavailable
builder.Services.AddHttpClient<FreightServiceHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
    // Automatically discover all consumers in the current assembly
    x.AddConsumersFromNamespaceContaining<FreightCreatedConsumer>();

    // Set endpoint name formatting to kebab-case (e.g., search-freight-created)
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq(
        (context, cnf) =>
        {
            //Adding Message retries for consumer
            //with intervals of 5 sec and 5 numbers of retries
            cnf.ReceiveEndpoint(
                "search-freight-created",
                e =>
                {
                    e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(5)));
                    //Specifying consumer
                    e.ConfigureConsumer<FreightCreatedConsumer>(context);
                }
            );

            cnf.ConfigureEndpoints(context);
        }
    );
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();

// Run DB initialization after the application starts
app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DbInitializer.InitDb(app);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"---> Error during database initialization: {ex.Message}");
    }
});

app.Run();

// Resilience policy: handles network failures and 404s by retrying every 3 seconds forever
static IAsyncPolicy<HttpResponseMessage> GetPolicy() =>
    HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
