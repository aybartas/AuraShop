using System.Collections.Immutable;
using AuraShop.Cargo.Business.Abstract;
using AuraShop.Cargo.Business.Concrete;
using AuraShop.Cargo.DataAccess;
using AuraShop.Cargo.DataAccess.Abstract;
using AuraShop.Cargo.DataAccess.Concrete;
using AuraShop.Cargo.Entity.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CargoDbContext>();

builder.Services.AddScoped<ICargoDal, CargoRepository>();
builder.Services.AddScoped<ICargoCompanyDal, CargoCompanyRepository>();
builder.Services.AddScoped<ICargoActionDal, CargoActionRepository>();
builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));

builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCargo"; 
    opt.RequireHttpsMetadata = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
