
namespace AuraShop.Order.Application.Features.CQRS.Commands.OrderLine
{
    public class CreateOrderLineCommand
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductTotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}
