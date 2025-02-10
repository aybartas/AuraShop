using AuraShop.Discount.Context;
using AuraShop.Discount.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraShop.Order.Persistence.Context
{
    public static class DbSeeder
    {
        public static void SeedDatabase(DapperContext dbContext)
        {
            if (!dbContext.Coupons.Any())
            {
                var orders = new List<Coupon>
                {
                    new()
                    {
                        Code="DISCOUNT 10",
                        ExpireDate= DateTime.Now,
                        IsActive= true,
                        Rate=10
                    },
                    new()
                    {
                        Code="DISCOUNT 20",
                        ExpireDate= DateTime.Now,
                        IsActive= true,
                        Rate=20
                    }
                };

                dbContext.Coupons.AddRange(orders);
                dbContext.SaveChanges();
            }
        }
    }
}



