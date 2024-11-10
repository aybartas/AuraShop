using AuraShop.Order.Application.Features.CQRS.Handlers.Address;
using AuraShop.Order.Application.Features.CQRS.Handlers.OrderLine;
using AuraShop.Order.Application.Interfaces;
using AuraShop.Order.Application.Services;
using AuraShop.Order.Persistence.Context;
using AuraShop.Order.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<OrderContext>();

#region 

builder.Services.AddScoped<GetAddressByIdQueryHandler>();
builder.Services.AddScoped<GetAddressQueryHandler>();
builder.Services.AddScoped<CreateAddressCommandHandler>();
builder.Services.AddScoped<UpdateAddressCommandHandler>();
builder.Services.AddScoped<RemoveAddressCommandHandler>();

builder.Services.AddScoped<GetOrderLineByIdQueryHandler>();
builder.Services.AddScoped<GetOrderLineQueryHandler>();
builder.Services.AddScoped<CreateOrderLineCommandHandler>();
builder.Services.AddScoped<UpdateOrderLineCommandHandler>();
builder.Services.AddScoped<RemoveOrderLineCommandHandler>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
