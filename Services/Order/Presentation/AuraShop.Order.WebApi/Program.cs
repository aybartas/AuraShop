using AuraShop.Order.API;
using AuraShop.Order.API.Endpoints.Orders;
using AuraShop.Order.Application;
using AuraShop.Order.Application.Contracts;
using AuraShop.Order.Application.UnitOfWork;
using AuraShop.Order.Persistence.Context;
using AuraShop.Order.Persistence.Repositories;
using AuraShop.Order.Persistence.UnitOfWork;
using AuraShop.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServices(typeof(OrderApplicationAssembly));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OrderContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

var versionSet = app.GetVersionSet();

app.AddOrderGroupEndpoints(versionSet);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
