using AuraShop.Catalog;
using AuraShop.Catalog.Database;
using AuraShop.Catalog.Features.Category;
using AuraShop.Catalog.Features.Product;
using AuraShop.Shared.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using AuraShop.Catalog.Features.Seed;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddSingleton<SeedService>();

builder.Services.AddAuthorization();

builder.Services.AddCommonServices(builder.Configuration,typeof(CatalogAssembly));

var app = builder.Build();

var versionSet = app.GetVersionSet();

app.AddCategoryGroupEndpoints(versionSet);
app.AddProductEndpoints(versionSet);

using (var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

    await seedService.SeedAsync();

}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
