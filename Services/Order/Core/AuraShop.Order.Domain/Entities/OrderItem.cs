
namespace AuraShop.Order.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string OrderNumber { get; set; }
        public Order Order { get; set; }
    } 
}
