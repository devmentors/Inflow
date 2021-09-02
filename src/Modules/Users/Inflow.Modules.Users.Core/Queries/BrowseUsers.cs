using Inflow.Modules.Users.Core.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Users.Core.Queries
{
    internal class BrowseUsers : PagedQuery<UserDto>
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
    }
}