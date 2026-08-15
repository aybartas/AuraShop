
namespace AuraShop.Bus.Events
{
    public record ProductImageUploadedEvent
    {
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}
