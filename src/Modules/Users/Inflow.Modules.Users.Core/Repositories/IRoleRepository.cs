using System.Collections.Generic;
using System.Threading.Tasks;
using Inflow.Modules.Users.Core.Entities;

namespace Inflow.Modules.Users.Core.Repositories
{
    internal interface IRoleRepository
    {
        Task<Role> GetAsync(string name);
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task AddAsync(Role role);
    }
}