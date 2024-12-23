﻿using BalanceMaster.Domain.Abstractions;
using BalanceMaster.FileRepository.Abstractions;
using BalanceMaster.FileRepository.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace BalanceMaster.FileRepository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection) => serviceCollection
        .AddScoped<ISequenceProvider, FileSequenceProvider>()
        .AddScoped<IOperationRepository, FileOperationRepository>()
        .AddScoped<IAccountRepository, FileAccountRepository>()
        .AddScoped<ICustomerRepository, FileCustomerRepository>();
}