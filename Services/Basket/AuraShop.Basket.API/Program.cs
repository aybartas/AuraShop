using System.Text.Json;
using AuraShop.Basket;
using AuraShop.Basket.Features.Baskets;
using AuraShop.Basket.Features.Baskets.ApplyDiscount;
using AuraShop.Shared.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServices(builder.Configuration, typeof(BasketAssembly));

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddScoped<BasketService>();
builder.Services.AddScoped<IBasketAuthService, BasketAuthService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddHttpClient<IDiscountService, DiscountService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["DiscountService:BaseUrl"]);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var versionSet = app.GetVersionSet();



app.AddBasketEndpoints(versionSet);

app.Run();

