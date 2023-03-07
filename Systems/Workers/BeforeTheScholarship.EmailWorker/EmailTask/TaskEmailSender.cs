using BeforeTheScholarship.DebtService;
using BeforeTheScholarship.StudentService;
using BeforeTheScholarship.Services.EmailSender;
using AutoMapper;
using BeforeTheScholarship.Common.Extensions;

namespace BeforeTheScholarship.EmailWorker.EmailTask;

public class TaskEmailSender : ITaskEmailSender
{
    private readonly ILogger<TaskEmailSender> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public TaskEmailSender(
        ILogger<TaskEmailSender> logger,
        IServiceProvider serviceProvider,
        IMapper mapper
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

    public async Task Start(bool isDevelopment)
    {
        while (true)
        {
            await Task.Delay(30_000);
            try
            {
                using var scope = _serviceProvider.CreateScope();
                
                var debtService = scope.ServiceProvider.GetRequiredService<IDebtService>();

                var debts = await debtService.GetDebts();

                foreach (var debt in debts)
                {
                    var totalDays = (debt.WhenToPayback - DateTimeOffset.Now.DateTime.ToLocalTime()).TotalDays;

                    _logger.LogInformation("TotalDays: {TotalDays}", totalDays);
                    if (totalDays < 1 && totalDays > 0 && !debt.EmailSended)
                    {
                        var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                        var student = await studentService.GetStudentById(debt.StudentId);

                        if (student.Email != null &&
                            emailService != null)
                        {
                            var content = PathReader.ReadContent(
                                                Path.Combine(Directory.GetCurrentDirectory(), "\\EmailTask\\EmailContentPages\\debtNotification.html"),
                                                "/app/emailpages/debtNotification.html")
                                                .Replace("DATETIMENOW", $"{DateTimeOffset.Now.DateTime.ToShortDateString()}")
                                                .Replace("STUDENTNAME", $"{student.UserName}")
                                                .Replace("BORROWED", $"{debt.Borrowed}")
                                                .Replace("WHENTOPAYBACK", $"{debt.WhenToPayback.DateTime.ToShortDateString()}"); ;

                            await emailService.SendEmail(
                                new EmailModel()
                                {
                                    EmailTo = student.Email,
                                    Subject = "One of your debts is about to expire",
                                    Message = content
                                });

                            debt.EmailSended = true;
                            await debtService.UpdateDebt(debt.Id, _mapper.Map<UpdateDebtModel>(debt));
                            _logger.LogInformation("Notification about debt sent to email({StudentEmail}).", student.Email);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
