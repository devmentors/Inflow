using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure;

public interface IInitializer
{
    Task InitAsync();
}