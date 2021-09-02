using System;
using Inflow.Modules.Users.Core.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Users.Core.Queries
{
    internal class GetUser : IQuery<UserDetailsDto>
    {
        public Guid UserId { get; set; }
    }
}