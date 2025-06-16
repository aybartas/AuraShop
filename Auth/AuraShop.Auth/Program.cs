using AuraShop.Auth;
using AuraShop.Auth.Features.Login;
using AuraShop.Auth.Features.Register;
using AuraShop.Auth.Services;
using Microsoft.Extensions.Configuration;

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

builder.Services.Configure<KeycloakConfig>(builder.Configuration.GetSection("Keycloak"));
builder.Services.AddHttpClient<KeycloakService>();

var app = builder.Build();

app.AddLoginEndpoint().AddRegisterEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
