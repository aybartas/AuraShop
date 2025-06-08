using System.Linq.Expressions;
using AuraShop.Order.Domain.Entities;

namespace AuraShop.Order.Application.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetByFilters(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
