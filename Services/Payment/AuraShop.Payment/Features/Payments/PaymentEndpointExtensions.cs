using Asp.Versioning.Builder;
using AuraShop.Payment.Features.Payments.CreatePayment;

namespace AuraShop.Payment.Features.Payments
{
    public static class PaymentEndpointExtensions
    {
        public static void AddPaymentEndpoints(this WebApplication app , ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("Payments")
                .CreatePayment()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
