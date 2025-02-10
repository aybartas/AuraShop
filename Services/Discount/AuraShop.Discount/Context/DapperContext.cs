using AuraShop.Discount.Entities;
using AuraShop.Order.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AuraShop.Discount.Context
{
    public class DapperContext : DbContext
    {      
        public DapperContext(DbContextOptions<DapperContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Coupon
            modelBuilder.Entity<Coupon>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Coupon>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Coupon> Coupons { get; set; }
       
    }
}
