using BalanceMaster.Api.Extensions;
using BalanceMaster.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder
    .AddJwtAuthentication()
    .AddSwaggerDocumentation()
    .AddApplicationServices()
    .AddReloadableAppSettings()
    .ConfigureFileStorageOptions()
    .AddSerilog()
    .AddDatabase()
    .AddIdentity();

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