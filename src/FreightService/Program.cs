using FreightService.Consumers;
using FreightService.Data;
using FreightService.RequestHelpers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FreightDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfiles).Assembly);
builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<FreightDbContext>(opt =>
    {
        // Check for new messages every 10s
        opt.QueryDelay = TimeSpan.FromSeconds(10);
        opt.UsePostgres(); //only for relational DB
        opt.UseBusOutbox();
    });

    //Handle faulty freifgt creation
    x.AddConsumersFromNamespaceContaining<FreightCreatedFaultConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("freight", false));

    x.UsingRabbitMq(
        (context, cnf) =>
        {
            cnf.ConfigureEndpoints(context);
        }
    );
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();

// Seeding Data properly
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<FreightDbContext>();
        // Ensure migrations are applied before seeding
        context.Database.Migrate();
        DbInitializer.InitDb(app);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during DB migration or seeding.");
    }
}

app.Run();
