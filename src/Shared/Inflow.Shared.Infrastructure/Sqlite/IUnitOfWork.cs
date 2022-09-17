using System;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Sqlite;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}