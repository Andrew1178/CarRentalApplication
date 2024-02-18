using System.Reflection;
using DataAccessLayer;
using BusinessLayer;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureClients(azureClientFactoryBuilder => azureClientFactoryBuilder.AddSecretClient(new Uri(builder.Configuration["KeyVaultUri"])));

builder.Services.AddScoped(typeof(IVehicleMakeService), typeof(VehicleMakeService));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
builder.Services.AddScoped(typeof(IOrderContentService), typeof(OrderContentService));
builder.Services.AddScoped(typeof(IVehicleService), typeof(VehicleService));
builder.Services.AddScoped(typeof(IKeyVaultRepository), typeof(KeyVaultRepository));
builder.Services.AddDbContext<CarRentalContext>();
builder.Services.AddScoped(typeof(IVehicleMakeRepository), typeof(VehicleMakeRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IOrderContentRepository), typeof(OrderContentRepository));
builder.Services.AddScoped(typeof(IVehicleRepository), typeof(VehicleRepository));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddAutoMapper(typeof(Program));

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
