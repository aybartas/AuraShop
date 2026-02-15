namespace AuraShop.Payment.Features.Payments;

public class PaymentDto
{
    public int Id { get; set; }
    public string PaymentReferenceId { get; set; } = default!;
    public string OrderNumber { get; set; } = default!;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreateDate { get; set; }
}