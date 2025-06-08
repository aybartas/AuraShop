namespace AuraShop.Order.Application.Dtos
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
