using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace AuraShop.Shared.Auth;

public static class EndpointAuthExtensions
{
    public static RouteHandlerBuilder RequireAdminRole(this RouteHandlerBuilder builder)
    {
        return builder.RequireAuthorization(Policies.AdminOnly);
    }

    public static RouteHandlerBuilder RequireCustomerRole(this RouteHandlerBuilder builder)
    {
        return builder.RequireAuthorization(Policies.CustomerOnly);
    }

    public static RouteHandlerBuilder RequireAuthenticated(this RouteHandlerBuilder builder)
    {
        return builder.RequireAuthorization(Policies.Authenticated);
    }

    public static IEndpointConventionBuilder RequireAdminRole(this IEndpointConventionBuilder builder)
    {
        return builder.RequireAuthorization(Policies.AdminOnly);
    }

    public static IEndpointConventionBuilder RequireCustomerRole(this IEndpointConventionBuilder builder)
    {
        return builder.RequireAuthorization(Policies.CustomerOnly);
    }

    public static IEndpointConventionBuilder RequireAuthenticated(this IEndpointConventionBuilder builder)
    {
        return builder.RequireAuthorization(Policies.Authenticated);
    }
}
