using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceMaster.Service.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(string message) : base(message)
    {
    }
}