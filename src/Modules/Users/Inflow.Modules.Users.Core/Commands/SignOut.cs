using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Users.Core.Commands;

internal record SignOut(Guid UserId) : ICommand;