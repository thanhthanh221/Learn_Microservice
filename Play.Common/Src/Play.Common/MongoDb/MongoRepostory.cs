using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using play.Common;
using Play.Common;

namespace Play.Common.MongoDb
{
    // Kế thừa được khi T kế thừa từ IEntity
    public class MongoRepository<T> : IRepository<T> where T: IEntity
    {
        private readonly IMongoCollection<T>? DbCollection;
        private readonly FilterDefinitionBuilder<T>? filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string CollectionName)
        {
            DbCollection = database.GetCollection<T>(CollectionName);
        }
        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await DbCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<T> GetAsync(Guid Id)
        {
            FilterDefinition<T>? filter = filterBuilder?.Eq(p => p.Id, Id);

            return await DbCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }
            await DbCollection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }
            FilterDefinition<T>? filter = filterBuilder?.Eq(itemlist => itemlist.Id, entity.Id);

            await DbCollection.ReplaceOneAsync(filter, entity);
        }
        public async Task DeleteAsync(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentException(nameof(Id));
            }
            FilterDefinition<T>? filter = filterBuilder?.Eq(itemlist => itemlist.Id, Id);

            await DbCollection.DeleteOneAsync(filter);


        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await DbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await DbCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}