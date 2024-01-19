using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Phlox.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController<ProductController>
    {

        /// <summary>
        /// Gets all the Products
        /// </summary>
        /// <returns>Returns a list of all Products</returns>
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            Logger.LogInformation("ProductController.GetAll called");

            return Context.GetAllProducts();
        }

        /// <summary>
        /// Gets a product based on ID.
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>Returns the product matching the ID specified. Returns 404 if the product does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            Product? product = Context.GetProductById(id);

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
        /// <returns>Returns the result of the create action.</returns>
        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            var newProduct = productDTO.ToModel();
            Context.CreateProduct(newProduct);

            Logger.LogInformation($"ProductController.Create created product {newProduct}");
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        /// <summary>
        /// Updates the specified Product.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="productDTO">The product dto.</param>
        /// <returns>Returns the result of the update action. Returns 404 if the product does not exist.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDTO productDTO)
        {
            Product product = productDTO.ToModel();
            product.Id = id;

            if (!Context.UpdateProduct(product))
            {
                Logger.LogInformation($"ProductController.Update did not find product with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"ProductController.Update updated product with an ID of {id}");
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="id">The identifier of the product to delete</param>
        /// <returns>Returns the result of the delete action. Returns 404 if the product does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Context.DeleteProduct(id))
            {
                Logger.LogInformation($"ProductController.Delete did not find product with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"ProductController.Update updated product with an ID of {id}");
            return NoContent();
        }
    }
}