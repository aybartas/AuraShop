using AuraShop.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Order.Persistence.Context
{
    public class OrderContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost,1434;initial Catalog=AuraShopOrderDb;User=sa;Password=Aurashop1.;TrustServerCertificate=true");
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<OrderLine> AddOrderLines { get; set; }
    }
}
