using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.DebtService;
using NSubstitute.ReturnsExtensions;

namespace BeforeTheScholarship.Tests.Integration.Helpers.CacheService
{
    public static class CacheServiceHelper
    {
        private static ICacheService _cacheService;

        public static ICacheService Initialize()
        {
            _cacheService = Substitute.For<ICacheService>();

            _cacheService.GetStringAsync<IEnumerable<DebtResponse>>(Arg.Any<string>()).ReturnsNull();
            _cacheService.SetStringAsync(Arg.Any<string>(), Arg.Any<IEnumerable<DebtResponse>>(), Arg.Any<TimeSpan>()).ReturnsNull();
            _cacheService.ClearStorage().Returns(Task.CompletedTask);

            return _cacheService;
        }
    }
}
