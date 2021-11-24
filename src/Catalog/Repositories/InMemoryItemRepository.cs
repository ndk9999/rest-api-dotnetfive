using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemoryItemRepository : IItemRepository
    {
        private readonly List<Item> _items = new()
        {
            new() { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };

        public Task<IEnumerable<Item>> GetItemsAsync()
        {
            return Task.FromResult(_items.AsEnumerable());
        }

        public Task<Item> GetItemAsync(Guid id)
        {
            var item = _items.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        public Task AddItemAsync(Item item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task UpdateItemAsync(Item item)
        {
            var idx = _items.FindIndex(x => x.Id == item.Id);

            if (idx >= 0) _items[idx] = item;

            return Task.CompletedTask;
        }

        public Task DeleteItemAsync(Guid id)
        {
            _items.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }
    }
}