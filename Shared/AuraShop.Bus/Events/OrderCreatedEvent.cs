namespace AuraShop.Bus.Events;

public record OrderCreatedEvent
{
    public int OrderId { get; set; }
    public Guid UserId { get; set; }
}