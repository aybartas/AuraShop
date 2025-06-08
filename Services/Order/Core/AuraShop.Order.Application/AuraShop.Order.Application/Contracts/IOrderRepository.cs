namespace AuraShop.Order.Application.Contracts;

public interface IOrderRepository : IGenericRepository<Domain.Entities.Order>
{
    Task<List<Domain.Entities.Order>> GetOrdersByUserId(Guid userId);
}