using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Extensions
{
    public static class ItemDealExtension
    {

        public static List<ItemDeal> GetAllItemDeals(this PhloxContext db)
        {
            return db.ItemDeal.ToList();
        }

        public static ItemDeal? GetItemDealById(this PhloxContext db, int itemId, int dealId)
        {
            return db.ItemDeal.FirstOrDefault(x => x.ItemId == itemId && x.DealId == dealId);
        }

        public static void CreateItemDeal(this PhloxContext db, ItemDeal itemDeal)
        {
            db.ItemDeal.Add(itemDeal);
            db.SaveChanges();
        }

        public static bool DeleteItemDeal(this PhloxContext db, int itemId, int dealId)
        {
            var itemDeal = db.GetItemDealById(itemId, dealId);

            if (itemDeal is null) return false;

            db.Remove(itemDeal);
            db.SaveChanges();

            return true;
        }
    }
}
