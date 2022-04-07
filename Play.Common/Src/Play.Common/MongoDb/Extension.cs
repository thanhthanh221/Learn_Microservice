using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Play.Common.MongoDb;
using Play.Common;
using Play.Common.Settings;

namespace play.Common.MongoDb
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection Services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // Chỉnh Giud thành String
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String)); // Ngày tháng thành String
            Services.AddSingleton(ServiceProvider =>
            {
                IConfiguration? Configuration = ServiceProvider.GetService<IConfiguration>();
                
                MongoDbSettings? mongoDbSettings = Configuration?.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                MongoClient? mongoClient = new MongoClient(mongoDbSettings?.ConnectionString);
                // Tên của DataBase
                ServiceSettings? serviceSettings = Configuration?.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

                return mongoClient.GetDatabase(serviceSettings?.ServiceName);
                    
            });
            return Services;
        }
        public static IServiceCollection AddMongoRepostory<T>(this IServiceCollection Services, string CollectionName) where T :IEntity
        {
            Services.AddSingleton<IRepository<T>>(serviceProvider => 
            {
                IMongoDatabase? database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, CollectionName);
            });
            return Services;
        }
    }
}