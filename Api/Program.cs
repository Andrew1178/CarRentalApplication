using System.Reflection;
using DataAccessLayer;
using BusinessLayer;
using Microsoft.Extensions.Azure;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

// TODO:
// 1. For some reason dotnet run is hanging, I presume because of the logger. I will need to investigate this further.
// 2. Fix the issue with the CurrentCultureInvariant case 
// I think it's the same problem.
// Hmm.. If I fix the CurrentCultureInVariant case issue, I dont think it will fix the logger.
// 3. ACTUALLY..
// The issue is my global exception handler is not being hit so Im not logging the error.
// So it 


// 3 issues
// 1. Adding the logger to connect to SQL server for some reason means swagger does not run
// 2. The logger is not actually logging to the SQL server (same issue as #1?)
// 2. My exception middleware is not being hit
// 3. I have an exception when trying to pull orders something about culture
// Note when I try to tell the serilog to create the table it throws an exception... Same one as #3, I will try fix it first

 Log.Logger = new LoggerConfiguration() // Create a "bootstrap" logger that can be used to log errors in the application startup process because if you only initialize once, it will not have access to dependency injection and the app settings.
     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
     .Enrich.FromLogContext()
     .WriteTo.Console()
     .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddAzureClients(azureClientFactoryBuilder => azureClientFactoryBuilder.AddSecretClient(new Uri(builder.Configuration["KeyVaultUri"])));

builder.Services.AddScoped(typeof(IVehicleMakeService), typeof(VehicleMakeService));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
builder.Services.AddScoped(typeof(IOrderContentService), typeof(OrderContentService));
builder.Services.AddScoped(typeof(IVehicleService), typeof(VehicleService));
builder.Services.AddDbContext<CarRentalContext>();
builder.Services.AddScoped(typeof(IVehicleMakeRepository), typeof(VehicleMakeRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IOrderContentRepository), typeof(OrderContentRepository));
builder.Services.AddScoped(typeof(IVehicleRepository), typeof(VehicleRepository));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddProblemDetails(); // Add standard problem details for error handling when showing the response to the user
 builder.Services.AddExceptionHandler<Api.GlobalExceptionHandler>(); // Add the global exception handler to the builder

var connectionString = builder.Configuration.GetConnectionString("CarRentalContext");

// // Replace the bootstrap logger with the actual logger that will log to the database
 builder.Host.UseSerilog((context, services, configuration) => configuration
     .ReadFrom.Configuration(context.Configuration)
     .ReadFrom.Services(services)
     .Enrich.FromLogContext()
     // .WriteTo.Console()); //When uncommenting this, the logger actually logs
      .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs"}));

var app = builder.Build();

app.UseStatusCodePages(); // When UseStatusCodePages isn't used, navigating to a URL without an endpoint returns a browser-dependent error message indicating the endpoint can't be found. When UseStatusCodePages is called, the browser returns Status Code: 404; Not Found

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else{
    app.UseExceptionHandler(); // Tell the app to use the global exception handler. Use it in Production/UAT because we want the dev to see the stack traces when an error is thrown when running in development
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
