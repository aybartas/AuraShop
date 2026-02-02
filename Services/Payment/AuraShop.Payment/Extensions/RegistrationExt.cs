using FluentValidation;
using Stripe;
using AuraShop.Payment.Features.Payments.CreatePayment;

namespace AuraShop.Payment.Extensions;

public static class RegistrationExt
{
    public static IServiceCollection AddPaymentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Stripe.StripeOptions>(configuration.GetSection("Stripe"));
        var stripeOptions = configuration.GetSection("Stripe").Get<Stripe.StripeOptions>() ?? new Stripe.StripeOptions();
        StripeConfiguration.ApiKey = stripeOptions.SecretKey;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommand).Assembly));
        services.AddValidatorsFromAssembly(typeof(PaymentAssembly).Assembly);
        return services;
    }
}
