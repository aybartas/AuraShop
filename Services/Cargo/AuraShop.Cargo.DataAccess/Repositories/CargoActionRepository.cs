using AuraShop.Cargo.DataAccess.Abstract;

namespace AuraShop.Cargo.DataAccess.Concrete;

public class CargoActionRepository : GenericRepository<Entity.Concrete.CargoAction>, ICargoActionDal
{
    public CargoActionRepository(CargoDbContext context) : base(context)
    {
    }
}