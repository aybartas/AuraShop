using AuraShop.Basket;
using AuraShop.Basket.Features.Baskets;
using AuraShop.Basket.Features.Baskets.ApplyDiscount;
using AuraShop.Shared.Auth;
using AuraShop.Shared.Extensions;

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

builder.Services.AddTransient<TokenForwardingDelegatingHandler>();
builder.Services.AddHttpClient<IDiscountService, DiscountService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:Discount"]
        ?? throw new InvalidOperationException("ServiceUrls:Discount not configured"));
})
.AddHttpMessageHandler<TokenForwardingDelegatingHandler>();

builder.Services.AddScoped<BasketService>();
builder.Services.AddMassTransit(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.UseDeveloperExceptionPage();

var versionSet = app.GetVersionSet();


app.AddBasketEndpoints(versionSet);

app.Run();

