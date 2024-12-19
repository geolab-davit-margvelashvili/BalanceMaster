using BalanceMaster.Api.Middlewares;
using BalanceMaster.Service.Extensions;
using BalanceMaster.Service.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();

var appOptions = new AppOptions
{
    AccountRepositoryPath = builder.Configuration.GetValue<string>("AccountRepositoryPath"),
};

builder.Services.AddSingleton(appOptions);

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