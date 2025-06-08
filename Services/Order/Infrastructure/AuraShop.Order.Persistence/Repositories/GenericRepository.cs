using AuraShop.Order.Application.Contracts;
using AuraShop.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AuraShop.Order.Domain.Entities;

namespace AuraShop.Order.Persistence.Repositories
{
    public class GenericRepository<T>(OrderContext context) : IGenericRepository<T>  where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<List<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task<List<T>> GetByFilters(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Remove(T entity) =>
            _dbSet.Remove(entity);

    }
}
