

using System.Linq.Expressions;

namespace AuraShop.Cargo.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task<T> Create(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter = null);
    }
}
