using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuraShop.Bus
{
    public static class ConfigurationExt
    {
        public static IServiceCollection AddCommonMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection("RabbitMQ").Get<BusOptions>() ?? throw new InvalidOperationException($"Failed to bind RabbitMQ section from configuration.");

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.Username);
                        host.Password(busOptions.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
