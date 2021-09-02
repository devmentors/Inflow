using System;
using System.ComponentModel.DataAnnotations;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Users.Core.Commands
{
    internal record SignIn([Required] [EmailAddress] string Email, [Required] string Password) : ICommand
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}