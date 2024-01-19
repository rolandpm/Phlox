
using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Phlox.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemDealController : BaseController<ItemDealController>
    {

        /// <summary>
        /// Gets all the ItemDeal
        /// </summary>
        /// <returns>Returns a list of all ItemDeals</returns>
        [HttpGet]
        public ActionResult<List<ItemDeal>> GetAll()
        {
            Logger.LogInformation("ItemDealController.GetAll called");

            return Context.GetAllItemDeals();
        }

        /// <summary>
        /// Gets a itemDeal based on ID.
        /// </summary>
        /// <param name="itemId">The itemDeal ID</param>
        /// <returns>Returns the itemDeal matching the ID specified. Returns 404 if the itemDeal does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<ItemDeal> GetById(int itemId, int dealId)
        {
            ItemDeal? itemDeal = Context.GetItemDealById(itemId, dealId);

            if (itemDeal is null)
            {
                Logger.LogInformation($"ItemDealController.Get did not find itemDeal with itemId of {itemId} and dealId of {dealId}");
                return NotFound();
            }

            Logger.LogInformation($"ItemDealController.Get found itemDeal {itemDeal}");
            return itemDeal;
        }

        /// <summary>
        /// Creates the specified itemDeal.
        /// </summary>
        /// <param name="itemDeal">The itemDeal.</param>
        /// <returns>Returns the result of the create action. Returns 404 if the userID specified in the itemDeal to create doesn't exist.</returns>
        [HttpPost]
        public IActionResult Create(ItemDealDTO itemDealDTO)
        {
            //If Item doesn't exist
            if (Context.GetItemById(itemDealDTO.ItemId) is null)
            {
                Logger.LogInformation($"ItemDealController.Create did not find Item {itemDealDTO.ItemId}");
                return NotFound("Item not found");
            }

            //If Deall doesn't exist
            if (Context.GetDealById(itemDealDTO.DealId) is null)
            {
                Logger.LogInformation($"ItemDealController.Create did not find ItemDeal with dealId of {itemDealDTO.DealId}");
                return NotFound("Deal not found");
            }

            var newItemDeal = itemDealDTO.ToModel();
            Context.CreateItemDeal(newItemDeal);

            Logger.LogInformation($"ItemDealController.Create created itemDeal {newItemDeal}");
            return CreatedAtAction(nameof(GetById), new { id = newItemDeal.ItemId }, newItemDeal);
        }

        /// <summary>
        /// Deletes the specified itemDeal.
        /// </summary>
        /// <param name="itemId">The identifier of the itemDeal to delete</param>
        /// <returns>Returns the result of the delete action. Returns 404 if the itemDeal does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int itemId, int dealId)
        {
            if (!Context.DeleteItemDeal(itemId, dealId))
            {
                Logger.LogInformation($"ItemDealController.Delete did not find itemDeal with an itemId of {itemId} and dealId of {dealId}");
                return NotFound("ItemDeal not found");
            }

            Logger.LogInformation($"ItemDealController.Update updated itemDeal with an itemId of {itemId} and dealId of {dealId}");
            return NoContent();
        }
    }
}