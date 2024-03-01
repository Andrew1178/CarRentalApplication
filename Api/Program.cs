using System.Reflection;
using DataAccessLayer;
using BusinessLayer;
using Microsoft.Extensions.Azure;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics;


 Log.Logger = new LoggerConfiguration() // Create a "bootstrap" logger that can be used to log errors in the application startup process because if you only initialize once, it will not have access to dependency injection and the app settings.
      .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
      .Enrich.FromLogContext()
      .WriteTo.Console()
      .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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
      .ReadFrom.Configuration(context.Configuration) // Read settings from appsettings.json
      .ReadFrom.Services(services)//  configure the logging pipeline with any registered implementations of the following services: IDestructuringPolicy, ILogEventEnricher, ILogEventFilter, ILogEventSink, LoggingLevelSwitch
      .Enrich.FromLogContext()
       .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(c => c.MessageTemplate.Text.StartsWith("Now listening on:")).WriteTo.Console()) // Write to the console here because otherwise the message be written to the DB and then SwaggerUI will not load automatically. This is a workaround and I believe is a bug with Serilog.
       .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", LevelSwitch = new Serilog.Core.LoggingLevelSwitch(LogEventLevel.Information) })); // Write to the database

var app = builder.Build();

app.UseSerilogRequestLogging(); // Exclude noisy handlers

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
