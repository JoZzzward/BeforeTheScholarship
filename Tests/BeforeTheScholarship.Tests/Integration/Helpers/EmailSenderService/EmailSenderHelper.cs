using BeforeTheScholarship.Services.EmailSender;

namespace BeforeTheScholarship.Tests.Integration.Helpers.EmailSenderService
{
    public static class EmailSenderHelper
    {
        private static IEmailSender _emailSender;

        public static IEmailSender Initialize()
        {
            _emailSender = Substitute.For<IEmailSender>();

            _emailSender.SendEmail(Arg.Any<EmailModel>()).Returns(Task.CompletedTask);

            return _emailSender;
        }
    }
}
