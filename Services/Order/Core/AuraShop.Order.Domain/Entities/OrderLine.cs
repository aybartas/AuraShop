
namespace AuraShop.Order.Domain.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductTotalPrice { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    } 
}
