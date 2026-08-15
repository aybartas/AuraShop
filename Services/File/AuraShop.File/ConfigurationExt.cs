using AuraShop.Bus;
using AuraShop.File.Consumers;
using MassTransit;

namespace AuraShop.File
{
    public static class ConfigurationExt
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection("RabbitMQ").Get<BusOptions>() ?? throw new InvalidOperationException($"Failed to bind RabbitMQ section from configuration.");

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<UploadProductImageCommandConsumer>();

                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.Username);
                        host.Password(busOptions.Password);
                    });

                    cfg.ReceiveEndpoint("file-microservice.upload-product-image-command.queue", e =>
                    {
                        e.ConfigureConsumer<UploadProductImageCommandConsumer>(context);
                    });

                });
            });

            return services;
        }
    }
}
