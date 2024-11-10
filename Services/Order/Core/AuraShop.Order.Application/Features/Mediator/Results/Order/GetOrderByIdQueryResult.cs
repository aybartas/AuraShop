
namespace AuraShop.Order.Application.Features.Mediator.Results.Order
{
    public class GetOrderByIdQueryResult
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
