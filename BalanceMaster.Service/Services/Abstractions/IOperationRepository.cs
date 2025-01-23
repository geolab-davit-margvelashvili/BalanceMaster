﻿using BalanceMaster.Domain.Models;

namespace BalanceMaster.Service.Services.Abstractions;

public interface IOperationRepository
{
    Task<Guid> CreateAsync(Operation operation);

    Task<Operation> GetByIdAsync(Guid id);
}