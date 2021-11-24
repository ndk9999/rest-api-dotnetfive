using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoItemRepository : IItemRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";

        private readonly IMongoCollection<Item> _items;

        public MongoItemRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _items = database.GetCollection<Item>(collectionName);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _items.Find(x => true).ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = Builders<Item>.Filter.Eq(item => item.Id, id);
            return await _items.Find(filter).SingleOrDefaultAsync();
        }

        public async Task AddItemAsync(Item item)
        {
            await _items.InsertOneAsync(item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            await _items.ReplaceOneAsync(x => x.Id == item.Id, item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await _items.DeleteOneAsync(x => x.Id == id);
        }
    }
}