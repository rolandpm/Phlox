using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealController(PhloxContext context) : BaseController<DealController>(context)
    {

        /// <summary>
        /// Gets all the deals
        /// </summary>
        /// <returns>List of all deals</returns>
        [HttpGet]
        public async Task<ActionResult<List<Deal>>> GetAll()
        {
            Logger.LogInformation("DealController.GetAll called");

            return await Context.Deal.ToListAsync();
        }

        /// <summary>
        /// Gets a deal based on ID.
        /// </summary>
        /// <param name="id">The deal ID</param>
        /// <returns>Deal with matching ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Deal>> Get(int id)
        {
            Deal? deal = await (
                from u in Context.Deal
                where u.Id == id
                select u
            ).FirstOrDefaultAsync();

            if (deal is null)
            {
                Logger.LogInformation($"DealController.Get did not find deal with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"DealController.Get found deal {deal}");
            return deal;
        }

        /// <summary>
        /// Creates the specified deal.
        /// </summary>
        /// <param name="deal">The deal.</param>
        /// <returns>Deal that was created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Deal deal)
        {
            Context.Add(deal);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"Deal {deal.Name} created");

            return CreatedAtAction(nameof(Get), new {id = deal.Id}, deal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Deal deal)
        {
            deal.Id = id;
            var result = await Get(id);
            var existingDeal = result.Value;

            if (existingDeal is null)
            {
                Logger.LogInformation($"Deal {deal.Name} not found");
                return NotFound();
            }

            Context.Entry(existingDeal).CurrentValues.SetValues(deal);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Get(id);
            var deal = result.Value;

            if (deal is null) return NotFound("Deal not found");

            Context.Remove(deal);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}