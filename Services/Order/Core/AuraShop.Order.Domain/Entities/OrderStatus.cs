namespace AuraShop.Order.Domain.Entities;

public enum OrderStatus
{
    PendingPayment,
    Paid,
    Shipped,
    Delivered,
    Canceled,
}