using AuraShop.Bus.Events;
using AuraShop.Catalog.Features.Product;
using MassTransit;

namespace AuraShop.Catalog.Consumers
{
    public class ProductImageUploadedEventConsumer : IConsumer<ProductImageUploadedEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductImageUploadedEventConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Consume(ConsumeContext<ProductImageUploadedEvent> context)
        {
           var message = context.Message;

           using var scope = _serviceProvider.CreateScope();
           var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

           var product = await productService.GetProductByIdAsync(context.Message.ProductId);

           product.Images ??= new List<string>();

           product.Images.Add(message.ImageUrl);

           await productService.UpdateProductAsync(product);

        }
    }
}
