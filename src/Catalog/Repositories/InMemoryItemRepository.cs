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

        public IEnumerable<Item> GetItems()
        {
            return _items;
        }

        public Item GetItem(Guid id)
        {
            return _items.SingleOrDefault(x => x.Id == id);
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var idx = _items.FindIndex(x => x.Id == item.Id);

            if (idx < 0) return;

            _items[idx] = item;
        }

        public void DeleteItem(Guid id)
        {
            _items.RemoveAll(x => x.Id == id);
        }
    }
}