using AuraShop.Cargo.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraShop.Cargo.DataAccess
{
    public class DbSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Cargo Companies
            modelBuilder.Entity<CargoCompany>().HasData(
                new CargoCompany { Id = 1,Name = "DHL" },
                new CargoCompany { Id = 2, Name = "FedEx" },
                new CargoCompany { Id = 3, Name = "UPS" }
            );

            // Seed Cargos
            modelBuilder.Entity<Entity.Concrete.Cargo>().HasData(
                new Entity.Concrete.Cargo
                {
                    Id = 1,
                    OrderNumber = "ORD123456",
                    TrackingNumber = 10001,
                    CargoCompanyId = 1,
                    Status = CargoStatus.InTransit,
                    ShippedDate = DateTime.UtcNow.AddDays(-3),
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(2),
                    DeliveredDate = null
                },
                new Entity.Concrete.Cargo
                {
                    Id = 2,
                    OrderNumber = "ORD123457",
                    TrackingNumber = 10002,
                    CargoCompanyId = 2,
                    Status = CargoStatus.Delivered,
                    ShippedDate = DateTime.UtcNow.AddDays(-5),
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(-1),
                    DeliveredDate = DateTime.UtcNow.AddDays(-1)
                }
            );

            // Seed Cargo Actions
            modelBuilder.Entity<CargoAction>().HasData(
                new CargoAction { Id = 1,  CargoId = 1, ActionDate = DateTime.UtcNow.AddDays(-3), Message = "Package picked up." },
                new CargoAction { Id = 2, CargoId = 1, ActionDate = DateTime.UtcNow.AddDays(-2), Message = "Package in transit." },
                new CargoAction { Id = 3, CargoId = 2, ActionDate = DateTime.UtcNow.AddDays(-5), Message = "Package picked up." },
                new CargoAction { Id = 4, CargoId = 2, ActionDate = DateTime.UtcNow.AddDays(-1), Message = "Package delivered." }
            );
        }
    }
}
