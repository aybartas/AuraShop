using AuraShop.Order.Application.UnitOfWork;
using AuraShop.Order.Persistence.Context;

namespace AuraShop.Order.Persistence.UnitOfWork
{
    public class UnitOfWork(OrderContext dbContext) : IUnitOfWork
    {
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
