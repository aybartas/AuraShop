using AuraShop.Basket;
using AuraShop.Basket.Features.Baskets;
using AuraShop.Basket.Features.Baskets.ApplyDiscount;
using AuraShop.Shared.Extensions;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServicesWithAuth(builder.Configuration, typeof(BasketAssembly));

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddScoped<BasketService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddHttpClient<IDiscountService, DiscountService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["DiscountService:BaseUrl"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

var versionSet = app.GetVersionSet();



app.AddBasketEndpoints(versionSet);

app.Run();

