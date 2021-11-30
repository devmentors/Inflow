using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Customers.Core.Commands;

internal record CreateCustomer(string Email) : ICommand;