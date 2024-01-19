using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Extensions
{
    public static class UserExtension
    {

        public static List<Users> GetAllUsers(this PhloxContext db)
        {
            return db.Users.ToList();
        }
        
        public static Users? GetUserById(this PhloxContext db, int id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        public static void CreateUser(this PhloxContext db, Users user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public static bool UpdateUser(this PhloxContext db, Users user)
        {
            var existingUser = db.GetUserById(user.Id);

            if (existingUser is null) return false;

            db.Entry(existingUser).CurrentValues.SetValues(user);
            db.SaveChanges();

            return true;
        }

        public static bool DeleteUser(this PhloxContext db, int id)
        {
            var existingUser = db.GetUserById(id);

            if (existingUser is null) return false;

            db.Remove(existingUser);
            db.SaveChanges();

            return true;
        }
    }
}
