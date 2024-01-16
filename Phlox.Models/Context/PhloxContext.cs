using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phlox.Models
{
    public class PhloxContext(DbContextOptions<PhloxContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityAlwaysColumns();
        }

        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<Item> Item { get; set; } = null!;
        public DbSet<ItemPhoto> ItemPhoto { get; set; } = null!;
        public DbSet<Deal> Deal { get; set; } = null!;
        public DbSet<ItemDeal> ItemDeal { get; set; } = null!;
        public DbSet<ExternalAccount> ExternalAccount { get; set; } = null!;
    }
}
