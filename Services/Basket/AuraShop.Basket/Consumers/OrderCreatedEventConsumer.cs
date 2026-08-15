using AuraShop.Basket.Features.Baskets;
using AuraShop.Bus.Events;
using MassTransit;

namespace AuraShop.Basket.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

            using var scope = serviceProvider.CreateScope();
            var productService = scope.ServiceProvider.GetRequiredService<BasketService>();

            await productService.RemoveBasketAsync(message.UserId);

        }
    }
}
