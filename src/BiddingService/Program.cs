using BiddingService.Consumers;
using BiddingService.RequestHelpers;
using BiddingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMassTransit(x =>
{
    //Handle faulty freifgt creation
    x.AddConsumersFromNamespaceContaining<FreightCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));

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

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfiles).Assembly);
builder.Services.AddHostedService<CheckTenderFinished>();

//Add gRPC client
builder.Services.AddScoped<GrpcFreightClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Initialize the database
await DB.InitAsync(
    "BidDb",
    MongoClientSettings.FromConnectionString(
        builder.Configuration.GetConnectionString("BidDbConnection")!
    )
);

app.Run();
