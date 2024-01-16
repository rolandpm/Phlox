using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemDealController(PhloxContext context) : BaseController<ItemDealController>(context)
    {

        /// <summary>
        /// Gets all the ItemDeal
        /// </summary>
        /// <returns>List of all ItemDeal</returns>
        [HttpGet]
        public async Task<ActionResult<List<ItemDeal>>> GetAll()
        {
            Logger.LogInformation("itemDealController.GetAll called");

            return await Context.ItemDeal.ToListAsync();
        }

        /// <summary>
        /// Gets a itemDeal based on ID.
        /// </summary>
        /// <param name="id">The itemDeal ID</param>
        /// <returns>itemDeal with matching ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDeal>> Get(int itemId, int dealId)
        {
            ItemDeal? itemDeal = await (
                from u in Context.ItemDeal
                where u.ItemId == itemId && u.DealId == dealId
                select u
            ).FirstOrDefaultAsync();

            if (itemDeal is null)
            {
                Logger.LogInformation($"itemDealController.Get did not find itemDeal");
                return NotFound();
            }

            Logger.LogInformation($"itemDealController.Get found itemDeal {itemDeal}");
            return itemDeal;
        }

        /// <summary>
        /// Creates the specified itemDeal.
        /// </summary>
        /// <param name="itemDeal">The itemDeal.</param>
        /// <returns>itemDeal that was created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ItemDeal itemDeal)
        {
            Context.Add(itemDeal);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"ItemDeal created");

            return CreatedAtAction(nameof(Get), 
                new {itemId = itemDeal.ItemId, dealId = itemDeal.DealId}, itemDeal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int itemId, int dealId)
        {
            var result = await Get(itemId, dealId);
            var itemDeal = result.Value;

            if (itemDeal is null) return NotFound("itemDeal not found");

            Context.Remove(itemDeal);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}