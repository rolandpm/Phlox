using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController(PhloxContext context) : BaseController<ItemController>(context)
    {

        /// <summary>
        /// Gets all the Item
        /// </summary>
        /// <returns>List of all Item</returns>
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            Logger.LogInformation("itemController.GetAll called");

            return await Context.Item.ToListAsync();
        }

        /// <summary>
        /// Gets a item based on ID.
        /// </summary>
        /// <param name="id">The item ID</param>
        /// <returns>item with matching ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            Item? item = await (
                from u in Context.Item
                where u.Id == id
                select u
            ).FirstOrDefaultAsync();

            if (item is null)
            {
                Logger.LogInformation($"itemController.Get did not find item with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"itemController.Get found item {item}");
            return item;
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>item that was created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            Context.Add(item);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"Item {item.Name} created");

            return CreatedAtAction(nameof(Get), new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            item.Id = id;
            var result = await Get(id);
            var existingitem = result.Value;

            if (existingitem is null) return NotFound();

            Context.Entry(existingitem).CurrentValues.SetValues(item);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Get(id);
            var item = result.Value;

            if (item is null) return NotFound("item not found");

            Context.Remove(item);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}