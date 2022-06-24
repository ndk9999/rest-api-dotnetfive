using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Extensions;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepo;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemRepository itemRepo, ILogger<ItemsController> logger)
        {
            _itemRepo = itemRepo;
            _logger = logger;
        }

        // GET /api/items/
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = await _itemRepo.GetItemsAsync();

            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items.Count()} items");

            return items.Select(x => x.AsDto());
        }

        // GET /api/items/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _itemRepo.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync([FromBody] CreateItemDto model)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _itemRepo.AddItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new{item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, [FromBody] UpdateItemDto model)
        {
            if (id != model.Id) return BadRequest();

            var item = await _itemRepo.GetItemAsync(id);

            if (item == null) return NotFound();

            var updatedItem = item with {
                Name = model.Name,
                Price = model.Price
            };
            await _itemRepo.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = await _itemRepo.GetItemAsync(id);

            if (item == null) return NotFound();

            await _itemRepo.DeleteItemAsync(id);

            return NoContent();
        }
    }
}