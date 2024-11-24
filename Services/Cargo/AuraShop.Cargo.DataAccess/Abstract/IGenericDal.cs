using System.Linq.Expressions;
using AuraShop.Cargo.Entity.Concrete;

namespace AuraShop.Cargo.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task<T> Create(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter = null);
    }

    public interface ICargoCompanyDal : IGenericDal<CargoCompany>
    {
    }

    public interface ICargoActionDal : IGenericDal<CargoAction>
    {
    }

    public interface ICargoDal : IGenericDal<Entity.Concrete.Cargo>
    {
    }

}
