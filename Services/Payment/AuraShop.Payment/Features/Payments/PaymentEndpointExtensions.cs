using Asp.Versioning.Builder;
using AuraShop.Payment.Features.Payments.CreatePayment;
using AuraShop.Payment.Features.Payments.GetAllByUser;
using AuraShop.Shared.Auth;

namespace AuraShop.Payment.Features.Payments
{
    public static class PaymentEndpointExtensions
    {
        public static void AddPaymentEndpoints(this WebApplication app , ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("Payments")
                .RequireAuthorization(Policies.Authenticated)
                .CreatePayment().GetAllByUser()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
