using System.Collections.Generic;

namespace Inflow.Shared.Infrastructure.Modules;

internal record ModuleInfo(string Name, IEnumerable<string> Policies);