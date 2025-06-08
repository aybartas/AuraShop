using Microsoft.EntityFrameworkCore;

namespace AuraShop.Order.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CouponCode { get; set; }
        public decimal? DiscountRate { get; set; }

        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; } = [];
        public string? PaymentId { get; set; }
        public OrderAddress ShippingAddress { get; set; } = null!;
        public decimal TotalPriceBeforeDiscount => Items.Sum(x => x.Quantity * x.UnitPrice);
        public decimal TotalPrice => TotalPriceBeforeDiscount - (TotalPriceBeforeDiscount * ((DiscountRate ?? 0) / 100));

        public static string GenerateOrderId()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var randomSuffix = new Random().Next(10000, 99999);

            return $"ORD-{timestamp}-{randomSuffix}";
        }

        public static Order CreateInitialOrder(Guid userId, OrderAddress address)
        {
            return new Order
            {
                OrderNumber = GenerateOrderId(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.PendingPayment,
                ShippingAddress = address,
            };
        }
        public void ApplyDiscount(string couponCode,decimal discountRate)
        {
            CouponCode = couponCode;
            DiscountRate = discountRate;
        }

        public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            var item = new OrderItem
            {
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity,
                OrderNumber = OrderNumber,
            };

            Items.Add(item);
        }

        public void SetPaid(string paymentId)
        {
            Status = OrderStatus.Paid;
            PaymentId = paymentId;
        }
    }

    [Owned]
    public class OrderAddress
    {
        public int? UserAddressId { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
