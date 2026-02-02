namespace AuraShop.Payment.Features.Payments.CreatePayment;

public class CreatePaymentResult
{
    public bool Success { get; set; }
    public string? PaymentId { get; set; }
    public string? Message { get; set; }
}
