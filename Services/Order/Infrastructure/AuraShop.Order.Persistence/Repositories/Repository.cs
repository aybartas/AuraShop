using System.Linq.Expressions;
using AuraShop.Order.Application.Interfaces;
using AuraShop.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Order.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OrderContext _context;
        public Repository(OrderContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var values =  await _context.Set<T>().ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var value = await _context.Set<T>().FindAsync(id);
            return value;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            var value = await _context.Set<T>()?.FirstOrDefaultAsync(filter);
            return value;
        }
    }
}
