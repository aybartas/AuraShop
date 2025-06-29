using AuraShop.Shared.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuraShop.Shared.Extensions
{
    public static class RegistrationExt
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddVersioning();
            services.AddAutoMapper(assembly);

            services.AddKeycloakAuth(configuration);

            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}
