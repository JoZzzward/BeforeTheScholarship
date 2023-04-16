using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.EmailSender;

namespace BeforeTheScholarship.Tests.Integration.Helpers.ActionsService
{
    public static class ActionsServiceHelper
    {
        private static IActionsService _actionsService;

        public static IActionsService Initialize()
        {
            _actionsService = Substitute.For<IActionsService>();

            _actionsService.SendDebtEmail(Arg.Any<DebtEmailModel>(), Arg.Any<double>()).Returns(Task.CompletedTask);

            return _actionsService;
        }
    }
}
