using AuraShop.Discount.Context;
using AuraShop.Discount.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

public class DbSeeder
{
    public static async Task SeedAsync(DapperContext context)
    {
        using var connection = context.CreateConnection();

        var tableCheckQuery = "IF NOT EXISTS (SELECT 1 FROM Coupons where Code=@Code ) BEGIN INSERT INTO Coupons (Code, Rate, IsActive, ExpireDate) VALUES (@Code, @Rate, @IsActive, @ExpireDate) END";

        var coupons = new List<Coupon>()
        {
            new()
            {
                Code = "DISCOUNT10",
                Rate = 10,
                IsActive = true,
                ExpireDate = DateTime.UtcNow.AddMonths(1)
            },
            new()
            {
                Code = "DISCOUNT20",
                Rate = 10,
                IsActive = true,
                ExpireDate = DateTime.UtcNow.AddMonths(1)
            },
        };

        foreach (var coupon in coupons)
        {
            await connection.ExecuteAsync(tableCheckQuery, coupon);
        }
    }
}