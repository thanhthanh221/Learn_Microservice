using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using play.Common;
using MongoDB.Driver;
using play.catalog.service.Entities;
using Play.Common.Settings;
using play.Common.MongoDb;
using MassTransit;
using MassTransit.MultiBus;
using Play.Common.MassTransit;
using Play.identity.Service.Extensions ;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
ServiceSettings? serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

builder.Services.AddMongoDb().AddMongoRepostory<Item>("Items").AddMassTransitWithRabbitMq();

builder.Services.AddControllers(option =>{
    option.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy  =>
                        {
                            policy.WithOrigins("http://localhost:3000",
                                              "http://www.contoso.com");
                        });
});
builder.Services.SetUpIdentity();
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

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
