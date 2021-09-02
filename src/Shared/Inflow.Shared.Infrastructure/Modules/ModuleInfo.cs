using System.Collections.Generic;

namespace Inflow.Shared.Infrastructure.Modules
{
    public record ModuleInfo(string Name, IEnumerable<string> Policies);
}