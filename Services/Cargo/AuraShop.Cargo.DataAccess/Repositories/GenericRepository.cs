using System.Linq.Expressions;
using AuraShop.Cargo.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Cargo.DataAccess.Concrete
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly CargoDbContext _context;

        private DbSet<T> table => _context.Set<T>();

        public GenericRepository(CargoDbContext context)
        {
            _context = context;
        }
        public async Task Create(T entity)
        {
            await table.AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        public  void Update(T entity)
        {
             table.Update(entity);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var value = await table.FindAsync(id);
            table.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            var value = await table.FindAsync(id);
            return value;
        }

        public async Task<List<T>> GetAll()
        {
            var values = await table.ToListAsync();
            return values;
        }

        public async Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
    }
}
