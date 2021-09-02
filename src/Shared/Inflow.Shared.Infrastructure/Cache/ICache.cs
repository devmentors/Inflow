using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Cache
{
    public interface ICache
    {
        Task<T> GetAsync<T>(string key);
        Task<IReadOnlyList<T>> GetManyAsync<T>(params string[] keys);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task DeleteAsync<T>(string key);
        Task AddToSetAsync<T>(string key, T value);
        Task DeleteFromSetAsync<T>(string key, T value);
        Task<bool> SetContainsAsync<T>(string key, T value);
    }
}