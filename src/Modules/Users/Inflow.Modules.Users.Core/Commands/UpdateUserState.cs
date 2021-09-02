using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Users.Core.Commands
{
    internal record UpdateUserState(Guid UserId, string State) : ICommand;
}