
using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Phlox.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemPhotoController : BaseController<ItemPhotoController>
    {

        /// <summary>
        /// Gets all the ItemPhoto
        /// </summary>
        /// <returns>Returns a list of all ItemPhotos</returns>
        [HttpGet]
        public ActionResult<List<ItemPhoto>> GetAll()
        {
            Logger.LogInformation("ItemPhotoController.GetAll called");

            return Context.GetAllItemPhotos();
        }

        /// <summary>
        /// Gets a itemPhoto based on ID.
        /// </summary>
        /// <param name="itemId">The itemPhoto ID</param>
        /// <returns>Returns the itemPhoto matching the ID specified. Returns 404 if the itemPhoto does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<ItemPhoto> GetById(int itemId)
        {
            ItemPhoto? itemPhoto = Context.GetItemPhotoById(itemId);

            if (itemPhoto is null)
            {
                Logger.LogInformation($"ItemPhotoController.Get did not find itemPhoto with an ID of {itemId}");
                return NotFound();
            }

            Logger.LogInformation($"ItemPhotoController.Get found itemPhoto {itemPhoto}");
            return itemPhoto;
        }

        /// <summary>
        /// Creates the specified itemPhoto.
        /// </summary>
        /// <param name="itemPhoto">The itemPhoto.</param>
        /// <returns>Returns the result of the create action. Returns 404 if the userID specified in the itemPhoto to create doesn't exist.</returns>
        [HttpPost]
        public IActionResult Create(ItemPhotoDTO itemPhotoDTO)
        {
            //If Item doesn't exist
            if (Context.GetItemById(itemPhotoDTO.ItemId) is null)
            {
                Logger.LogInformation($"ItemPhotoController.Create did not find Item with an ID of {itemPhotoDTO.ItemId}");
                return NotFound("Item not found");
            }

            var newItemPhoto = itemPhotoDTO.ToModel();
            Context.CreateItemPhoto(newItemPhoto);

            Logger.LogInformation($"ItemPhotoController.Create created itemPhoto {newItemPhoto}");
            return CreatedAtAction(nameof(GetById), new { id = newItemPhoto.ItemId }, newItemPhoto);
        }

        /// <summary>
        /// Updates the specified itemPhoto.
        /// </summary>
        /// <param name="itemId">The ID of the itemPhoto.</param>
        /// <param name="itemPhotoDTO">The itemPhoto dto.</param>
        /// <returns>Returns the result of the update action. Returns 404 if the itemPhoto does not exist.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int itemId, ItemPhotoDTO itemPhotoDTO)
        {
            ItemPhoto itemPhoto = itemPhotoDTO.ToModel();
            itemPhoto.ItemId = itemId;

            if (!Context.UpdateItemPhoto(itemPhoto))
            {
                Logger.LogInformation($"ItemPhotoController.Update did not find itemPhoto with an ID of {itemId}");
                return NotFound("ItemPhoto not found");
            }

            Logger.LogInformation($"ItemPhotoController.Update updated itemPhoto with an ID of {itemId}");
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified itemPhoto.
        /// </summary>
        /// <param name="itemId">The identifier of the itemPhoto to delete</param>
        /// <returns>Returns the result of the delete action. Returns 404 if the itemPhoto does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int itemId)
        {
            if (!Context.DeleteItemPhoto(itemId))
            {
                Logger.LogInformation($"ItemPhotoController.Delete did not find itemPhoto with an ID of {itemId}");
                return NotFound("ItemPhoto not found");
            }

            Logger.LogInformation($"ItemPhotoController.Update updated itemPhoto with an ID of {itemId}");
            return NoContent();
        }
    }
}