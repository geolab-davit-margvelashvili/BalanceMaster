using BalanceMaster.Api.Extensions;
using BalanceMaster.Api.Middlewares;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder
    .AddJwtAuthentication()
    .AddSwaggerDocumentation()
    .AddApplicationServices()
    .AddReloadableAppSettings()
    .ConfigureFileStorageOptions();

//var logger = new LoggerConfiguration()
//    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
//    .WriteTo.File(path: "/logs/", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog as the logging provider

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseErrorHandlingMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();