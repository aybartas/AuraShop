namespace AuraShop.Basket.Dtos
{
    public class BasketItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public int TotalQuantity => BasketItems.Sum(x => x.Quantity);
        public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);

        public List<BasketItemDto> BasketItems { get; set; }
    }
}
