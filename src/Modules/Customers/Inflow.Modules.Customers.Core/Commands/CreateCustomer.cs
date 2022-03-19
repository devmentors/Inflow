using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Customers.Core.Commands;

public record CreateCustomer(string Email) : ICommand;
