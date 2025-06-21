using AuraShop.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.GetProfile
{
    public static class GetProfileEndpointExt
    {
        public static RouteGroupBuilder AddGetProfileEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapGet("/profile", (GetProfileHandler handler, IHttpContextAccessor httpContextAccessor) =>
            {
                var response =  handler.HandleAsync(httpContextAccessor);
                return response.ToResult();
            }).RequireAuthorization();

            return builder;
        }
    }
}
