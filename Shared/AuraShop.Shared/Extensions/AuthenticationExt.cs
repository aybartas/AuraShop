using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;
using AuraShop.Shared.Configs;

namespace AuraShop.Shared.Extensions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddKeycloakAuth(this IServiceCollection services , IConfiguration configuration)
        {
            services.Configure<KeycloakConfig>(configuration.GetSection("Keycloak"));

            var keycloakConfig = configuration.GetSection("Keycloak").Get<KeycloakConfig>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = $"{keycloakConfig.BaseUrl.TrimEnd('/')}/realms/{keycloakConfig.Realm}";
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"{keycloakConfig.BaseUrl.TrimEnd('/')}/realms/{keycloakConfig.Realm}",
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;

                        if (claimsIdentity == null) return Task.CompletedTask;
                        var realmRoles = context.Principal?.FindFirst("realm_access")?.Value;
                        if (realmRoles == null) return Task.CompletedTask;

                        using var doc = JsonDocument.Parse(realmRoles);
                        var roles = doc.RootElement.GetProperty("roles").EnumerateArray();
                        foreach (var role in roles)
                        {
                            claimsIdentity.AddClaim(new Claim("role", role.GetString() ?? ""));
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine(context.Exception.Message);
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new { error = "Authentication failed", details = context.Exception.Message });
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            return services;
        }
    }
}
