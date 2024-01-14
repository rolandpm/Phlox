using Microsoft.EntityFrameworkCore;

namespace Phlox.Models
{
    public partial class PhloxContext : DbContext
    {
        public PhloxContext(DbContextOptions<PhloxContext> options) : base(options) { }

        public DbSet<Users> users { get; set; } = null!;
        public DbSet<Product> product { get; set; } = null!;
        public DbSet<Item> item { get; set; } = null!;
        public DbSet<Item_Photo> item_photo { get; set; } = null!;
        public DbSet<Deal> deal { get; set; } = null!;
        public DbSet<Available_Deal> available_deal { get; } = null!;
        public DbSet<External_Account> external_account { get; set; } = null!;
    }
}
