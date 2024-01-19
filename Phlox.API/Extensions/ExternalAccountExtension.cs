using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Extensions
{
    public static class ExternalAccountExtension
    {
        public static List<ExternalAccount> GetAllExternalAccounts(this PhloxContext db)
        {
            return db.ExternalAccount.ToList();
        }
        
        public static ExternalAccount? GetExternalAccountById(this PhloxContext db, int id)
        {
            return db.ExternalAccount.FirstOrDefault(x => x.Id == id);
        }

        public static void CreateExternalAccount(this PhloxContext db, ExternalAccount externalAccount)
        {
            db.ExternalAccount.Add(externalAccount);
            db.SaveChanges();
        }

        public static bool UpdateExternalAccount(this PhloxContext db, ExternalAccount externalAccount)
        {
            var existingExternalAccount = GetExternalAccountById(db, externalAccount.Id);

            if (existingExternalAccount is null) return false;

            db.Entry(existingExternalAccount).CurrentValues.SetValues(externalAccount);
            db.SaveChanges();

            return true;
        }

        public static bool DeleteExternalAccount(this PhloxContext db, int id)
        {
            var existingExternalAccount = GetExternalAccountById(db, id);

            if (existingExternalAccount is null) return false;

            db.Remove(existingExternalAccount);
            db.SaveChanges();

            return true;
        }
    }
}
