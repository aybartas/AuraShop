using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuraShop.Shared.Auth;

public static class KeycloakAuthExtensions
{
    public static IServiceCollection AddKeycloakAuthentication(  this IServiceCollection services, IConfiguration configuration)
    {
        var keycloakSettings = configuration
            .GetSection(KeycloakSettings.SectionName)
            .Get<KeycloakSettings>()
            ?? throw new InvalidOperationException("Keycloak settings not configured");

        services.Configure<KeycloakSettings>(configuration.GetSection(KeycloakSettings.SectionName));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = keycloakSettings.RealmUrl;
            options.Audience = keycloakSettings.Audience;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = keycloakSettings.RealmUrl,
                ValidateAudience = true,
                ValidAudiences = [keycloakSettings.Audience],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RoleClaimType = "roles",
                NameClaimType = ClaimTypes.Name
            };

            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    MapKeycloakRolesToClaims(context);
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        context.Response.Headers["Token-Expired"] = "true";
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorizationBuilder()
            .AddPolicy(Policies.AdminOnly, policy =>
                policy.RequireRole(Roles.Admin))
            .AddPolicy(Policies.CustomerOnly, policy =>
                policy.RequireRole(Roles.Customer))
            .AddPolicy(Policies.Authenticated, policy =>
                policy.RequireAuthenticatedUser());

        return services;
    }

    private static void MapKeycloakRolesToClaims(TokenValidatedContext context)
    {
        if (context.Principal?.Identity is not ClaimsIdentity claimsIdentity)
            return;

        // Map realm roles from "roles" claim
        MapRolesFromClaim(claimsIdentity, "roles");

        // Map roles from realm_access.roles (Keycloak standard)
        MapRolesFromRealmAccess(claimsIdentity, context.Principal);

        // Map roles from resource_access (client roles)
        MapRolesFromResourceAccess(claimsIdentity, context.Principal);
    }

    private static void MapRolesFromClaim(ClaimsIdentity identity, string claimType)
    {
        var rolesClaims = identity.FindAll(claimType).ToList();
        foreach (var roleClaim in rolesClaims)
        {
            if (!identity.HasClaim(ClaimTypes.Role, roleClaim.Value))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
            }
        }
    }

    private static void MapRolesFromRealmAccess(ClaimsIdentity identity, ClaimsPrincipal principal)
    {
        var realmAccessClaim = principal.FindFirst("realm_access");
        if (realmAccessClaim?.Value == null) return;

        try
        {
            using var doc = System.Text.Json.JsonDocument.Parse(realmAccessClaim.Value);
            if (doc.RootElement.TryGetProperty("roles", out var rolesElement))
            {
                foreach (var role in rolesElement.EnumerateArray())
                {
                    var roleValue = role.GetString();
                    if (!string.IsNullOrEmpty(roleValue) &&
                        !identity.HasClaim(ClaimTypes.Role, roleValue))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                    }
                }
            }
        }
        catch
        {
            // Silently ignore JSON parsing errors
        }
    }

    private static void MapRolesFromResourceAccess(ClaimsIdentity identity, ClaimsPrincipal principal)
    {
        var resourceAccessClaim = principal.FindFirst("resource_access");
        if (resourceAccessClaim?.Value == null) return;

        try
        {
            using var doc = System.Text.Json.JsonDocument.Parse(resourceAccessClaim.Value);
            foreach (var client in doc.RootElement.EnumerateObject())
            {
                if (client.Value.TryGetProperty("roles", out var rolesElement))
                {
                    foreach (var role in rolesElement.EnumerateArray())
                    {
                        var roleValue = role.GetString();
                        if (!string.IsNullOrEmpty(roleValue) &&
                            !identity.HasClaim(ClaimTypes.Role, roleValue))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                        }
                    }
                }
            }
        }
        catch
        {
            // Silently ignore JSON parsing errors
        }
    }
}
