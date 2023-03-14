using BeforeTheScholarship.Services.Settings;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace BeforeTheScholarship.Services.CacheService;

public class CacheService : ICacheService
{
    private readonly ILogger<CacheService> _logger;
    private readonly CacheSettings _settings;
    private IDatabase cacheStorage;

    private readonly ConnectionMultiplexer _redis;

    public CacheService(
        ILogger<CacheService> logger,
        CacheSettings settings
        )
    {
        _logger = logger;
        _settings = settings;

        _redis = ConnectionMultiplexer.Connect(
        $"{_settings.Host}:{_settings.Port},password={_settings.Password}"
        );

        cacheStorage = _redis.GetDatabase();
    }

    public async Task<T?> GetStringAsync<T>(string key)
    {
        _logger.LogInformation("Getting data with key({Key}) from cache", key);
        string? cachedData = await cacheStorage.StringGetAsync(key);

        if (cachedData is null)
        {
            _logger.LogInformation("Cannot get cached data with specified key({Key})", key);
            return default;
        }   

        var data = JsonSerializer.Deserialize<T>(cachedData);

        _logger.LogInformation("Cached data with specified key({Key}) was returned successfully", key);
        return data;
    }

    public async Task SetStringAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
    {
        _logger.LogInformation("Trying to set data with key({Key}) to cache", key);
        var jsonData = JsonSerializer.Serialize(data);

        await cacheStorage.StringSetAsync(key, jsonData, absoluteExpireTime ?? _settings.CacheLifetime);
        _logger.LogInformation("Cached data with specified key({Key}) was set successfully", key);
    }
}
