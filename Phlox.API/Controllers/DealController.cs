//TODDO: COMMENTS

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
        /// Gets all the products
        /// </summary>
        /// <returns>List of all products</returns>
        [HttpGet]
        public async Task<ActionResult<List<Deal>>> GetAll()
        {
            Logger.LogInformation("DealController.GetAll called");

            return await Context.Deal.ToListAsync();
        }

        /// <summary>
        /// Gets a product based on ID.
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>Deal with matching ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Deal>> Get(int id)
        {
            Deal? product = await (
                from u in Context.Deal
                where u.Id == id
                select u
            ).FirstOrDefaultAsync();

            if (product is null)
            {
                Logger.LogInformation($"DealController.Get did not find product with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"DealController.Get found product {product}");
            return product;
        }

        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Deal that was created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Deal product)
        {
            Context.Add(product);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"Deal {product.Name} created");

            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Deal product)
        {
            product.Id = id;
            var result = await Get(id);
            var existingDeal = result.Value;

            if (existingDeal is null)
            {
                Logger.LogInformation($"Deal {product.Name} not found");
                return NotFound();
            }

            Context.Entry(existingDeal).CurrentValues.SetValues(product);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Get(id);
            var product = result.Value;

            if (product is null) return NotFound("Deal not found");

            Context.Remove(product);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}