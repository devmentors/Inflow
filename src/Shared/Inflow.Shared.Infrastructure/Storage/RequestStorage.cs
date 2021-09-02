using System;
using Microsoft.Extensions.Caching.Memory;
using Inflow.Shared.Abstractions.Storage;

namespace Inflow.Shared.Infrastructure.Storage
{
    public class RequestStorage : IRequestStorage
    {
        private readonly IMemoryCache _cache;

        public RequestStorage(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set<T>(string key, T value, TimeSpan? duration = null) 
            => _cache.Set(key, value, duration ?? TimeSpan.FromSeconds(5));

        public T Get<T>(string key) => _cache.Get<T>(key);
    }
}