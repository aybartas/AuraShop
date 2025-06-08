using AuraShop.Order.Domain.Entities;
using AuraShop.Order.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Order.Persistence.Context
{
    public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> AddOrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
        }
    }
}
