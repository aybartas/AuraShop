
namespace AuraShop.Payment.Features.Payments.GetAllByUser;

public class GetAllByUserResult
{
    public int Id { get; set; }
    public string PaymentReferenceId { get; set; }
    public string OrderNumber { get; set; } = null!;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}