using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Phlox.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : BaseController<ItemController>
    {

        /// <summary>
        /// Gets all the Item
        /// </summary>
        /// <returns>Returns a list of all Items</returns>
        [HttpGet]
        public ActionResult<List<Item>> GetAll()
        {
            Logger.LogInformation("ItemController.GetAll called");

            return Context.GetAllItems();
        }

        /// <summary>
        /// Gets a item based on ID.
        /// </summary>
        /// <param name="id">The item ID</param>
        /// <returns>Returns the item matching the ID specified. Returns 404 if the item does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<Item> GetById(int id)
        {
            Item? item = Context.GetItemById(id);

            if (item is null)
            {
                Logger.LogInformation($"ItemController.Get did not find item with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"ItemController.Get found item {item}");
            return item;
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns the result of the create action.</returns>
        [HttpPost]
        public IActionResult Create(ItemDTO itemDTO)
        {
            //If user doesn't exist
            if (Context.GetUserById(itemDTO.UserId) is null)
            {
                Logger.LogInformation($"ItemController.Create did not find User with an ID of {itemDTO.UserId}");
                return NotFound("User not found");
            }
            //If product doesn't exist
            if (Context.GetProductById(itemDTO.ProductId) is null)
            {
                Logger.LogInformation($"ItemController.Create did not find Product with an ID of {itemDTO.ProductId}");
                return NotFound("Product not found");
            }

            var newItem = itemDTO.ToModel();
            Context.CreateItem(newItem);

            Logger.LogInformation($"ItemController.Create created item {newItem}");
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        /// <summary>
        /// Updates the specified External Account.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <param name="itemDTO">The item dto.</param>
        /// <returns>Returns the result of the update action. Returns 404 if the item does not exist.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, ItemDTO itemDTO)
        {
            Item item = itemDTO.ToModel();
            item.Id = id;

            if (!Context.UpdateItem(item))
            {
                Logger.LogInformation($"ItemController.Update did not find item with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"ItemController.Update updated item with an ID of {id}");
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="id">The identifier of the item to delete</param>
        /// <returns>Returns the result of the delete action. Returns 404 if the item does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Context.DeleteItem(id))
            {
                Logger.LogInformation($"ItemController.Delete did not find item with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"ItemController.Update updated item with an ID of {id}");
            return NoContent();
        }
    }
}