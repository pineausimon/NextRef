namespace NextRef.Application.Caching;
public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey);
    Task SetAsync<T>(string cacheKey, T data, TimeSpan? expiry = null);
    Task RemoveByPatternAsync(string pattern);
}