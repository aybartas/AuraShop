using FluentValidation;
using Stripe;
using AuraShop.Payment.Features.Payments.CreatePayment;
using AuraShop.Payment.Features.Payments;

namespace AuraShop.Payment.Extensions;

public static class RegistrationExt
{
    public static IServiceCollection AddPaymentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StripeOptions>(configuration.GetSection("Stripe"));
        var stripeOptions = configuration.GetSection("Stripe").Get<StripeOptions>() ?? new StripeOptions();
        StripeConfiguration.ApiKey = stripeOptions.SecretKey;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommand).Assembly));
        services.AddValidatorsFromAssembly(typeof(PaymentAssembly).Assembly);
        return services;
    }
}
