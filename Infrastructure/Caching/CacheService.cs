using Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken) where T : class
        {
            string? cacheValue = await _distributedCache.GetStringAsync(key, cancellationToken);
            
            if(cacheValue is null)
            {
                return null;
            }

            T? value = JsonSerializer.Deserialize<T>(cacheValue);
            return value;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) where T : class
        {
            T? cacheValue = await GetAsync<T>(key, cancellationToken);

            if(cacheValue is not null)
            {
                return cacheValue;
            }

            cacheValue = await factory();

            await SetAsync(key, cacheValue, cancellationToken);

            return cacheValue;
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            string cacheValue = JsonSerializer.Serialize(key);
            return _distributedCache.RemoveAsync(cacheValue, cancellationToken);
        }

         public Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
        {
            // TODO: Implement the logic to remove cache entries by prefix key
            throw new NotImplementedException();
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            string cacheValue = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, cacheValue, cancellationToken);
        }
    }
}
