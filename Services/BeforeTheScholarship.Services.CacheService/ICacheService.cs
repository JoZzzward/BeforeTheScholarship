using StackExchange.Redis;

namespace BeforeTheScholarship.Services.CacheService;

public interface ICacheService
{
    Task<T?> GetStringAsync<T>(string key);
    Task SetStringAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null);
    Task ClearStorage();
}