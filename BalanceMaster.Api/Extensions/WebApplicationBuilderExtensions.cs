using BalanceMaster.FileRepository.Models;
using BalanceMaster.Service.Extensions;
using BalanceMaster.SqlRepository.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace BalanceMaster.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddSwaggerDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.ExampleFilters(); // ამატებს მაგალითებს სვაგერი დოკუმენტაციაში
        });
        // არეგისტრირებს მაგალთის კლასებს სერვისის კოლექციაში
        builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

        return builder;
    }

    public static WebApplicationBuilder AddReloadableAppSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        return builder;
    }

    public static WebApplicationBuilder ConfigureFileStorageOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorageOptions"));

        return builder;
    }

    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddJwtBearerAuthentication(builder.Configuration);
        return builder;
    }

    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddServices()
            .AddSqlRepositories();

        return builder;
    }
}