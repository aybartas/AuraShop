namespace AuraShop.Payment.Features.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public string? PaymentReferenceId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
    
        public static Payment Create(string paymentReferenceId,PaymentStatus status, Guid userId, string orderNumber, decimal amount)
        {
            return new Payment()
            {
                UserId = userId,
                OrderNumber = orderNumber,
                Amount = amount,
                CreateDate = DateTime.UtcNow,
                Status = status,
                PaymentReferenceId = paymentReferenceId
            };
        }
    }

    public enum PaymentStatus
    {
        Success,
        Failed
    }
}
