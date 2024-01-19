using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Extensions
{
    public static class DealExtension
    {

        public static List<Deal> GetAllDeals(this PhloxContext db)
        {
            return db.Deal.ToList();
        }

        public static Deal? GetDealById(this PhloxContext db, int id)
        {
            return db.Deal.FirstOrDefault(x => x.Id == id);
        }

        public static void CreateDeal(this PhloxContext db, Deal deal)
        {
            db.Deal.Add(deal);
            db.SaveChanges();
        }

        public static bool UpdateDeal(this PhloxContext db, Deal deal)
        {
            var existingDeal = db.GetDealById(deal.Id);

            if (existingDeal is null) return false;

            db.Entry(existingDeal).CurrentValues.SetValues(deal);
            db.SaveChanges();

            return true;
        }

        public static bool DeleteDeal(this PhloxContext db, int id)
        {
            var deal = db.GetDealById(id);

            if (deal is null) return false;

            db.Remove(deal);
            db.SaveChanges();

            return true;
        }
    }
}
