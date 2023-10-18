using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.ServiceProcess;
using TWAB.Api.Db;
using TWAB.Api.Services;
using TWAB.Api.Settings;
using TWAB.Models.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
        serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<ProductService>();
//builder.Services.AddScoped<IMongoDbSettings, MongoDbSettings>();

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

/*using (var scope = app.Services.CreateScope())
{
    var productRepo = scope.ServiceProvider.GetRequiredService<MongoRepository<Product>>();
    var products = await scope.ServiceProvider.GetRequiredService<ProductService>().GetAllProducts();
    foreach (var p in products)
    {
        productRepo.InsertOneAsync(p);
    }
}*/

app.Run();
