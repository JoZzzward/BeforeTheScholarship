using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Tests.Unit.Base.Services.Debts;
using NSubstitute.ClearExtensions;
using NSubstitute.ReturnsExtensions;

namespace BeforeTheScholarship.Tests.Unit.Helpers.Configuration
{
    public static class CacheServiceInitializer
    {
        private static readonly DebtsDataHelper _dataHelper = new();
        private static ICacheService _cacheService;

        public static ICacheService Initialize()
        {
            _cacheService = Substitute.For<ICacheService>();

            return _cacheService;
        }

        public static class CacheServiceSetup
        {
            public static void SetupGetStringReturnsNull()
            {
                _cacheService.ClearSubstitute();
                _cacheService.GetStringAsync<IEnumerable<DebtResponse>>(Arg.Any<string>()).ReturnsNull();
            }

            public static void SetupGetStringReturnsData()
            {
                _cacheService.ClearSubstitute();
                var data = _dataHelper.GenerateDebtResponses();
                _cacheService.GetStringAsync<IEnumerable<DebtResponse>>(Arg.Any<string>()).Returns(data);
            }

            public static async Task<ICacheService> SetupSetStringByReturnsCompletedTask()
            {
                _cacheService.SetStringAsync(Arg.Any<string>(), Arg.Any<Type>()).Returns(Task.CompletedTask);

                return _cacheService;
            }

            public static void SetupClearStorage()
            {
                _cacheService.ClearStorage().Returns(Task.CompletedTask);
            }
        }
    }
}
