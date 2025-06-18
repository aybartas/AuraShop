using AuraShop.Auth;
using AuraShop.Auth.Features;
using AuraShop.Auth.Features.GetProfile;
using AuraShop.Auth.Features.Login;
using AuraShop.Auth.Features.Register;
using AuraShop.Auth.Models;
using AuraShop.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<KeycloakService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<KeycloakService>();

// Register handlers as scoped services
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<GetProfileHandler>();

builder.Services.Configure<KeycloakConfig>(builder.Configuration.GetSection("Keycloak"));
builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<KeycloakConfig>>().Value;
    return new KeycloakEndpoints(options);
});

builder.Services.AddHttpClient<KeycloakService>();

var keycloakConfig = builder.Configuration.GetSection("Keycloak").Get<KeycloakConfig>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{keycloakConfig.BaseUrl.TrimEnd('/')}/realms/{keycloakConfig.Realm}";
        options.RequireHttpsMetadata = false; // Use true in production

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
                // Log the exception, or do other custom logic
                Console.WriteLine(context.Exception.Message);


                // Optionally, modify the response
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = "Authentication failed", details = context.Exception.Message });
                return context.Response.WriteAsync(result);
            }
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();


app.UseAuthentication(); 
app.UseAuthorization();

app.AddAuthEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
