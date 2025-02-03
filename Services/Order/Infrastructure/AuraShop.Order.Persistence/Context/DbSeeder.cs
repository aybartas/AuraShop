using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraShop.Order.Domain.Entities;

namespace AuraShop.Order.Persistence.Context
{
    public static class DbSeeder
    {
        public static void SeedDatabase(OrderDbContext dbContext)
        {
            if (!dbContext.Orders.Any()) // Prevent duplicate seeding
            {
                var orders = new List<Domain.Entities.Order>
                {
                    new Domain.Entities.Order
                    {
                        UserId = "user1",
                        TotalPrice = 250.00m,
                        OrderDate = DateTime.UtcNow,
                        Address = new OrderAddress
                        {
                            District = "Kadikoy",
                            City = "Istanbul",
                            Detail = "Street 123"
                        },
                        OrderLines = new List<OrderLine>
                        {
                            new OrderLine { ProductId = "P100", ProductName = "Laptop", ProductPrice = 120.00m, ProductTotalPrice = 240.00m },
                            new OrderLine { ProductId = "P101", ProductName = "Mouse", ProductPrice = 10.00m, ProductTotalPrice = 10.00m }
                        }
                    },
                    new Domain.Entities.Order
                    {
                        UserId = "user2",
                        TotalPrice = 180.00m,
                        OrderDate = DateTime.UtcNow,
                        Address = new OrderAddress
                        {
                            District = "Sisli",
                            City = "Istanbul",
                            Detail = "Street 456"
                        },
                        OrderLines = new List<OrderLine>
                        {
                            new OrderLine { ProductId = "P200", ProductName = "Keyboard", ProductPrice = 90.00m, ProductTotalPrice = 180.00m }
                        }
                    }
                };

                dbContext.Orders.AddRange(orders);
                dbContext.SaveChanges();
            }
        }
    }
}



