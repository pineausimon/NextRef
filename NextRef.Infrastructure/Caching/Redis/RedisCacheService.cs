using StackExchange.Redis;
using System.Text.Json;
using NextRef.Application.Caching;
using NextRef.Infrastructure.Serialization;

namespace NextRef.Infrastructure.Caching.Redis;
public class RedisCacheService : ICacheService
{
    private static readonly JsonSerializerOptions _jsonOptions = JsonOptionsFactory.Create();

    private readonly IDatabase _cache;
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _cache = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string cacheKey)
    {
        var cached = await _cache.StringGetAsync(cacheKey);
        if (cached.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>(cached, _jsonOptions);

    }

    public async Task SetAsync<T>(string cacheKey, T data, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        await _cache.StringSetAsync(cacheKey, json, expiry ?? TimeSpan.FromMinutes(5));
    }

    public async Task RemoveByPatternAsync(string pattern)
    {
        // Récupère le premier endpoint (suppose un seul serveur Redis)
        var endpoints = _redis.GetEndPoints();
        var server = _redis.GetServer(endpoints.First());

        // Attention : KEYS peut être coûteux sur de gros datasets
        var keys = server.Keys(pattern: pattern).ToArray();
        if (keys.Length == 0) return;

        foreach (var key in keys)
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}