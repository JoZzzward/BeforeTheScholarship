using BeforeTheScholarship.Common.CacheConstKeys;
using BeforeTheScholarship.Common.Exceptions;
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

    private static string redisUri;
    private static ConnectionMultiplexer Connection => lazyConnection.Value;
    private static Lazy<ConnectionMultiplexer> lazyConnection = new(() => ConnectionMultiplexer.Connect(redisUri));

    public CacheService(
        ILogger<CacheService> logger,
        CacheSettings settings
        )
    {
        _logger = logger;
        _settings = settings;

        redisUri = _settings.Uri;

        cacheStorage = Connection.GetDatabase();
    }

    public async Task<bool> RemoveByKey(string key ) => await cacheStorage.KeyDeleteAsync( key );

    public async Task<T?> GetStringAsync<T>(string key)
    {
        _logger.LogInformation("--> Getting data with Key ({Key}) from cache", key);
        string? cachedData = await cacheStorage.StringGetAsync(key);

        if (cachedData is null)
        {
            _logger.LogInformation("--> Cannot get cached data with specified Key ({Key})", key);
            return default;
        }

        var data = JsonSerializer.Deserialize<T>(cachedData);

        _logger.LogInformation("--> Cached data with specified Key ({Key}) was returned successfully", key);
        return data;
    }

    public async Task SetStringAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
    {
        _logger.LogInformation("--> Trying to set data with Key ({Key}) to cache", key);
        var jsonData = JsonSerializer.Serialize(data);

        await cacheStorage.StringSetAsync(key, jsonData, absoluteExpireTime ?? TimeSpan.FromMinutes(_settings.CacheLifetime));
        _logger.LogInformation("--> Cached data with specified Key ({Key}) was set successfully", key);
    }

    public async Task ClearStorage()
    {
        await RemoveByKey(DebtsCacheKeys.AllDebtsKey);
        await RemoveByKey(DebtsCacheKeys.DebtsWithSpecifiedStudentKey);
        await RemoveByKey(DebtsCacheKeys.UrgentlyRepaidDebtsKey);
        await RemoveByKey(DebtsCacheKeys.OverdueDebtsKey);

        _logger.LogInformation("--> Storage was successfully cleared");
    }
}
