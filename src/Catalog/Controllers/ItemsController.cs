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

        public ItemsController(IItemRepository itemRepo)
        {
            _itemRepo = itemRepo;
        }

        // GET /api/items/
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return _itemRepo.GetItems().Select(x => x.AsDto());
        }

        // GET /api/items/id
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _itemRepo.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem([FromBody] CreateItemDto model)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _itemRepo.AddItem(item);

            return CreatedAtAction(nameof(GetItem), new{item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, [FromBody] UpdateItemDto model)
        {
            if (id != model.Id) return BadRequest();

            var item = _itemRepo.GetItem(id);

            if (item == null) return NotFound();

            var updatedItem = item with {
                Name = model.Name,
                Price = model.Price
            };
            _itemRepo.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var item = _itemRepo.GetItem(id);

            if (item == null) return NotFound();

            _itemRepo.DeleteItem(id);

            return NoContent();
        }
    }
}