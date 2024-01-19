using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Extensions
{
    public static class ItemExtension
    {

        public static List<Item> GetAllItems(this PhloxContext db)
        {
            return db.Item.ToList();
        }

        public static Item? GetItemById(this PhloxContext db, int id)
        {
            return db.Item.FirstOrDefault(x => x.Id == id);
        }

        public static void CreateItem(this PhloxContext db, Item item)
        {
            db.Item.Add(item);
            db.SaveChanges();
        }

        public static bool UpdateItem(this PhloxContext db, Item item)
        {
            var existingItem = db.GetItemById(item.Id);

            if (existingItem is null) return false;

            db.Entry(existingItem).CurrentValues.SetValues(item);
            db.SaveChanges();

            return true;
        }

        public static bool DeleteItem(this PhloxContext db, int id)
        {
            var item = db.GetItemById(id);

            if (item is null) return false;

            db.Remove(item);
            db.SaveChanges();

            return true;
        }
    }
}
