using BalanceMaster.Service.Services.Abstractions;
using BalanceMaster.SqlRepository.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace BalanceMaster.SqlRepository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlRepositories(this IServiceCollection services) => services
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped<IAccountRepository, AccountRepository>()
        .AddScoped<IOperationRepository, OperationRepository>();
}