namespace API.Cache.Interfaces
{
    public interface ICacheService
    {
        object? GetCacheByKey(string cacheKey);
        void AddCache(string cacheKey, object value);


    }
}
