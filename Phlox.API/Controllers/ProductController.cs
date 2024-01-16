using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(PhloxContext context) : BaseController<ProductController>(context)
    {

        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>List of all products</returns>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            Logger.LogInformation("ProductController.GetAll called");

            return await Context.Product.ToListAsync();
        }

        /// <summary>
        /// Gets a product based on ID.
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>Product with matching ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            Product? product = await (
                from u in Context.Product
                where u.Id == id
                select u
            ).FirstOrDefaultAsync();

            if (product is null)
            {
                Logger.LogInformation($"ProductController.Get did not find product with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"ProductController.Get found product {product}");
            return product;
        }

        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Product that was created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            Context.Add(product);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"Product {product.Name} created");

            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            product.Id = id;
            var result = await Get(id);
            var existingProduct = result.Value;

            if (existingProduct is null)
            {
                Logger.LogInformation($"Product {product.Name} not found");
                return NotFound();
            }

            Context.Entry(existingProduct).CurrentValues.SetValues(product);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Get(id);
            var product = result.Value;

            if (product is null) return NotFound("Product not found");

            Context.Remove(product);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}