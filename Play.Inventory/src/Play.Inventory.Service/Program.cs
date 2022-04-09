using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Clients;
using Polly;
using Microsoft.Extensions.Logging;
using Polly.Timeout;
using Play.Common.MongoDb;
using play.Common.MongoDb;
using Play.Common;
using Play.Common.MassTransit;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMongoDb().AddMongoRepostory<InventoryItem>("InventoryItems")
    .AddMongoRepostory<CatalogItem>("catalogitems")
    .AddMassTransitWithRabbitMq();
    
AddCatalogClient(builder);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddCatalogClient(WebApplicationBuilder builder)
{
    Random? jitterer = new Random();

    IServiceCollection? service = builder.Services;
    builder.Services.AddHttpClient<CataLogClient>(client =>
    {
        client.BaseAddress = new Uri("https://localhost:7070");
    })

    .AddTransientHttpErrorPolicy(builder =>
        builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
            5,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)),
            onRetry: (outcome, timespan, retryAttempt) =>
            {
                ServiceProvider serviceProvider = service.BuildServiceProvider();

                // Thông báo dòng cảnh báo
                serviceProvider.GetService<ILogger<CataLogClient>>()?
                    .LogWarning($"Bi Delay {timespan.TotalSeconds} thu lai {retryAttempt}");
            }
        )
    )
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
}