﻿using BalanceMaster.Service.Commands;

namespace BalanceMaster.Service.Services.Abstractions;

public interface ICustomerService
{
    Task<int> ExecuteAsync(RegisterCustomerCommand command);

    Task ExecuteAsync(OpenCustomerCommand command);

    Task ExecuteAsync(CloseCustomerCommand command);

    Task ExecuteAsync(SuspendCustomerCommand command);
}