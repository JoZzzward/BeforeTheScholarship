using BeforeTheScholarship.Services.CacheService;
using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Tests.Integration.Helpers.CacheService
{
    public static partial class Bootstrapper
    {
        public static void AddFakeCacheService(this IServiceCollection services)
        {
            var cacheService = CacheServiceHelper.Initialize();

            services.AddSingleton(typeof(ICacheService), cacheService);

        }
    }
}
