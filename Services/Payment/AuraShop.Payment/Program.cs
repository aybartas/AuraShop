using AuraShop.Payment;
using AuraShop.Payment.Database;
using AuraShop.Payment.Extensions;
using AuraShop.Payment.Features.Payments;
using AuraShop.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPaymentServices(builder.Configuration);
builder.Services.AddCommonServicesWithAuth(builder.Configuration, typeof(PaymentAssembly));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();
var versionSet = app.GetVersionSet();

app.AddPaymentEndpoints(versionSet);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

