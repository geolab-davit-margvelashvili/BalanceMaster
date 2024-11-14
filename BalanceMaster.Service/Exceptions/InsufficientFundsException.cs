using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMaster.Service.Exceptions;

public class InsufficientFundsException : DomainException
{
    public InsufficientFundsException(int accountId)
        : base($"Insufficient funds on account with id: {accountId}")
    {
    }
}