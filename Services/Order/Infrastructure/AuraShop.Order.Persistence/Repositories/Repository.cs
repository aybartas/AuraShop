using System.Linq.Expressions;
using AuraShop.Order.Application.Interfaces;
using AuraShop.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Order.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OrderDbContext _dbContext;
        public Repository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var values =  await _dbContext.Set<T>().ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var value = await _dbContext.Set<T>().FindAsync(id);
            return value;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            var value = await _dbContext.Set<T>()?.FirstOrDefaultAsync(filter);
            return value;
        }
    }
}
