
namespace AuraShop.Order.Application.UnitOfWork
{
    public interface IUnitOfWork 
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
