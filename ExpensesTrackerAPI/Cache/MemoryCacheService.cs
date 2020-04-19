using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesTrackerAPI.Cache
{
    using System.Collections.Concurrent;
    using System.Threading;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheService<T> : IMemoryCacheService<T>  
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public async Task<T> GetOrCreate(object key, Func<Task<T>> createItem)
        {
            if (_cache.TryGetValue(key, out T cacheEntry))
            {
                return cacheEntry;
            }

            var padlock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

            await padlock.WaitAsync();
            try
            {
                if (!_cache.TryGetValue(key, out cacheEntry))
                {
                    // Key not in cache, so get data.
                    cacheEntry = await createItem();
                    _cache.Set(key, cacheEntry);
                }
            }
            finally
            {
                padlock.Release();
            }
            return cacheEntry;
        }
    }
}
