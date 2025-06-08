using AuraShop.Order.Application.Contracts;
using AuraShop.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Order.Persistence.Repositories
{
    public class OrderRepository(OrderContext context) : GenericRepository<Domain.Entities.Order>(context),IOrderRepository
    {
        public async Task<List<Domain.Entities.Order>> GetOrdersByUserId(Guid userId)
        {
            return await context.Orders.Include(x => x.Items).Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
