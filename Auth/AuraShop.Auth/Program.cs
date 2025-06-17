using AuraShop.Auth;
using AuraShop.Auth.Features.GetProfile;
using AuraShop.Auth.Features.Login;
using AuraShop.Auth.Features.Register;
using AuraShop.Auth.Models;
using AuraShop.Auth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<KeycloakService>();
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

var app = builder.Build();

app.AddLoginEndpoint().AddRegisterEndpoint().AddGetProfileEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
