using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Extensions
{
    public static class ProductExtension
    {

        public static List<Product> GetAllProducts(this PhloxContext db)
        {
            return db.Product.ToList();
        }

        public static Product? GetProductById(this PhloxContext db, int id)
        {
            return db.Product.FirstOrDefault(x => x.Id == id);
        }

        public static void CreateProduct(this PhloxContext db, Product product)
        {
            db.Product.Add(product);
            db.SaveChanges();
        }

        public static bool UpdateProduct(this PhloxContext db, Product product)
        {
            var existingProduct = db.GetProductById(product.Id);

            if (existingProduct is null) return false;

            db.Entry(existingProduct).CurrentValues.SetValues(product);
            db.SaveChanges();

            return true;
        }

        public static bool DeleteProduct(this PhloxContext db, int id)
        {
            var product = db.GetProductById(id);

            if (product is null) return false;

            db.Remove(product);
            db.SaveChanges();

            return true;
        }
    }
}
