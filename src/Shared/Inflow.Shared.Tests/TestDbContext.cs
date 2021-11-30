using System;
using Microsoft.EntityFrameworkCore;

namespace Inflow.Shared.Tests;

public abstract class TestDbContext<T> where T : DbContext, IDisposable
{
    public T Context { get; }

    protected TestDbContext(string connectionString = null)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Context = Init(connectionString);
            
        if (Context is null)
        {
            throw new InvalidOperationException($"DB context for {typeof(T)} was not initialized.");
        }
    }

    protected abstract T Init(string connectionString);

    public void Dispose()
    {
        Context?.Database.EnsureDeleted();
        Context?.Dispose();
    }
}