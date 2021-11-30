using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Users.Core.Commands;

internal record ChangePassword(Guid UserId, string CurrentPassword, string NewPassword) : ICommand;