using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Customers.Core.Commands
{
    internal record CompleteCustomer(Guid CustomerId, string Name, string FullName, string Address, string Nationality,
        string IdentityType, string IdentitySeries) : ICommand;
}