using AuraShop.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Order.Persistence.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Address
            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id); 

            modelBuilder.Entity<Address>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            // Order
            modelBuilder.Entity<Domain.Entities.Order>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Domain.Entities.Order>()
                .OwnsOne(a => a.Address);

            modelBuilder.Entity<Domain.Entities.Order>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            // Order Line
            modelBuilder.Entity<OrderLine>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<OrderLine>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<OrderLine> AddOrderLines { get; set; }
    }
}
