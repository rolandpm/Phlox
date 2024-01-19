using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Extensions
{
    public static class ItemPhotoExtension
    {

        public static List<ItemPhoto> GetAllItemPhotos(this PhloxContext db)
        {
            return db.ItemPhoto.ToList();
        }

        public static ItemPhoto? GetItemPhotoById(this PhloxContext db, int ItemId)
        {
            return db.ItemPhoto.FirstOrDefault(x => x.ItemId == ItemId);
        }

        public static void CreateItemPhoto(this PhloxContext db, ItemPhoto itemPhoto)
        {
            db.ItemPhoto.Add(itemPhoto);
            db.SaveChanges();
        }

        public static bool UpdateItemPhoto(this PhloxContext db, ItemPhoto itemPhoto)
        {
            var existingItemPhoto = db.GetItemPhotoById(itemPhoto.ItemId);

            if (existingItemPhoto is null) return false;

            db.Entry(existingItemPhoto).CurrentValues.SetValues(itemPhoto);
            db.SaveChanges();

            return true;
        }

        public static bool DeleteItemPhoto(this PhloxContext db, int ItemId)
        {
            var itemPhoto = db.GetItemPhotoById(ItemId);

            if (itemPhoto is null) return false;

            db.Remove(itemPhoto);
            db.SaveChanges();

            return true;
        }
    }
}
