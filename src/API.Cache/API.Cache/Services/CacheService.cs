using API.Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace API.Cache.Services
{
    public class CacheService : ICacheService
    {
        private readonly IServiceProvider _serviceProvider;

        public CacheService(IServiceProvider serviceProvider)
        {
                _serviceProvider = serviceProvider;

        }

        public void AddCache(string cacheKey, object value)
        {
            var memooryCache = _serviceProvider.GetRequiredService<IMemoryCache>();

            MemoryCacheEntryOptions opt = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            };

            memooryCache.Set(cacheKey, value);
        }

        public object? GetCacheByKey(string cacheKey)
        {
            
            var memooryCache = _serviceProvider.GetRequiredService<IMemoryCache>();

            MemoryCacheEntryOptions opt = new MemoryCacheEntryOptions()
            { 
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            };

            memooryCache.TryGetValue(cacheKey, out object? value);
            if (value != null) 
            {
                return value;
            }

            return null;
        }
    }
}
