using System;
using System.ComponentModel.DataAnnotations;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Users.Core.Commands
{
    internal record SignUp([Required] [EmailAddress] string Email, [Required] string Password, string Role) : ICommand
    {
        public Guid UserId { get; init; } = Guid.NewGuid();
    }
}