using AuraShop.Cargo.DataAccess.Abstract;

namespace AuraShop.Cargo.DataAccess.Concrete;

public class CargoCompanyRepository : GenericRepository<Entity.Concrete.CargoCompany>, ICargoCompanyDal
{
    public CargoCompanyRepository(CargoDbContext context) : base(context)
    {
    }
}