using Inflow.Modules.Users.Core.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Users.Core.Queries
{
    internal class GetUserByEmail : IQuery<UserDetailsDto>
    {
        public string Email { get; set; }
    }
}