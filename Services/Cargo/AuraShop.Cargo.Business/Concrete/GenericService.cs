using System.Linq.Expressions;
using AuraShop.Cargo.Business.Abstract;
using AuraShop.Cargo.DataAccess.Abstract;

namespace AuraShop.Cargo.Business.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericDal<T> _genericDal;
        public GenericService(IGenericDal<T> genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<T> Create(T entity)
        {
            var value = await _genericDal.Create(entity);
            return value;
        }

        public void Update(T entity)
        {
            _genericDal.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _genericDal.Delete(id);
        }

        public async Task<T> GetById(int id)
        {
            var value = await _genericDal.GetById(id);
            return value;
        }

        public async Task<List<T>> GetAll()
        {
            return await _genericDal.GetAll();
        }

        public async Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter = null)
        {
            return await _genericDal.GetByFilter(filter);
        }
    }
}
