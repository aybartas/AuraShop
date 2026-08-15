using AuraShop.Basket.Consumers;
using AuraShop.Bus;
using MassTransit;

namespace AuraShop.Basket
{
    public static class ConfigurationExt
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection("RabbitMQ").Get<BusOptions>() ?? throw new InvalidOperationException($"Failed to bind RabbitMQ section from configuration.");

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<OrderCreatedEventConsumer>();

                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.Username);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("basket-microservice.order-created.queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
                    });

                });
            });

            return services;
        }
    }
}
