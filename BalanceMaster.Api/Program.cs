using BalanceMaster.Api;
using BalanceMaster.Api.Middlewares;
using BalanceMaster.FileRepository.Extensions;
using BalanceMaster.FileRepository.Models;
using BalanceMaster.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddServices()
    .AddRepositories();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorageOptions"));

builder.Services.Configure<DatabaseOptions>(DatabaseOptions.SystemDatabaseSectionName,
    builder.Configuration.GetSection($"{DatabaseOptions.SectionName}:{DatabaseOptions.SystemDatabaseSectionName}"));

builder.Services.Configure<DatabaseOptions>(DatabaseOptions.BusinessDatabaseSectionName,
    builder.Configuration.GetSection($"{DatabaseOptions.SectionName}:{DatabaseOptions.BusinessDatabaseSectionName}"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseErrorHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();