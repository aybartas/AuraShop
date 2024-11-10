using AuraShop.Cargo.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraShop.Cargo.DataAccess
{
    public class CargoDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CargoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo.Entity.Concrete.Cargo>().
                Property(x => x.Status).HasConversion<string>();
        }

        public List<Entity.Concrete.Cargo> Cargos { get; set; }
        public List<CargoCompany> CargoCompanies { get; set; }
        public List<CargoAction> CargoActions { get; set; }
    }
}
