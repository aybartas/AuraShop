using AuraShop.Cargo.DataAccess.Abstract;

namespace AuraShop.Cargo.DataAccess.Concrete;

public class CargoRepository: GenericRepository<Entity.Concrete.Cargo> , ICargoDal
{
    public CargoRepository(CargoDbContext context) : base(context)
    {
    }
}