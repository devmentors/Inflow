using System;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Postgres
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}