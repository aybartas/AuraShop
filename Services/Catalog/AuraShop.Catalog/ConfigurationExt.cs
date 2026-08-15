using AuraShop.Bus;
using AuraShop.Bus.Events;
using AuraShop.Catalog.Consumers;
using MassTransit;

namespace AuraShop.Catalog
{
    public static class ConfigurationExt
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection("RabbitMQ").Get<BusOptions>() ?? throw new InvalidOperationException($"Failed to bind RabbitMQ section from configuration.");

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<ProductImageUploadedEventConsumer>();

                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.Username);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("catalog-microservice.course-picture-uploaded.queue", e =>
                    {
                        e.ConfigureConsumer<ProductImageUploadedEventConsumer>(context);
                    });

                });
            });

            return services;
        }
    }
}
