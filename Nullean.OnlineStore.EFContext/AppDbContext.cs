using Microsoft.EntityFrameworkCore;
using Nullean.OnlineStore.EFContext.EfEntities;

namespace Nullean.OnlineStore.EFContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
