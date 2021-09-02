using System;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Users.Core.Entities
{
    internal class User
    {
        public Guid Id { get; set; }
        public Email Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string RoleId { get; set; }
        public UserState State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}