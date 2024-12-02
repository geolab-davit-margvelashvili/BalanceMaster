﻿using BalanceMaster.Service.Commands;
using BalanceMaster.Service.Models;

namespace BalanceMaster.Service.Mappings;

public static class OperationMappings
{
    public static Operation ToOperation(this DebitCommand command) =>
        Operation.CreateDebitOperation(command.AccountId, command.Amount, command.Currency);

    public static Operation ToOperation(this CreditCommand command) =>
        Operation.CreateCreditOperation(command.AccountId, command.Amount, command.Currency);
}