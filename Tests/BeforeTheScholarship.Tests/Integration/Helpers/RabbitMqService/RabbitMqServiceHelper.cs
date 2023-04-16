using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.RabbitMqService;
using BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers;

namespace BeforeTheScholarship.Tests.Integration.Helpers.RabbitMqService
{
    public static class RabbitMqServiceHelper
    {
        private static readonly DebtsDataHelper _dataHelper = new();
        private static IRabbitMqService _rabbitMqService;

        public static IRabbitMqService Initialize()
        {
            _rabbitMqService = Substitute.For<IRabbitMqService>();

            _rabbitMqService.PushAsync(
                    Arg.Any<string>(), Arg.Any<IEnumerable<DebtResponse>>(), Arg.Any<double>())
                .Returns(Task.CompletedTask);

            _rabbitMqService.Subscribe(
                Arg.Any<string>(), 
                Arg.Any<OnMessageReceive<Func<bool>>>())
                .Returns(Task.CompletedTask);

            return _rabbitMqService;
        }
    }
}
