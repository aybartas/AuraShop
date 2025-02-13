using AuraShop.Cargo.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Cargo.DataAccess
{
    public class CargoDbContext : DbContext
    {
        public CargoDbContext(DbContextOptions<CargoDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cargo
            modelBuilder.Entity<Entity.Concrete.Cargo>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Entity.Concrete.Cargo>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Cargo.Entity.Concrete.Cargo>().
                Property(x => x.Status).HasConversion<string>();

            //CargoAction

            modelBuilder.Entity<CargoAction>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<CargoAction>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            // CargoCompany
            modelBuilder.Entity<CargoCompany>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<CargoCompany>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            DbSeeder.Seed(modelBuilder);
        }

        public List<Entity.Concrete.Cargo> Cargos { get; set; }
        public List<CargoCompany> CargoCompanies { get; set; }
        public List<CargoAction> CargoActions { get; set; }
    }
}
