using BeforeTheScholarship.DebtService;
using BeforeTheScholarship.StudentService;
using BeforeTheScholarship.Services.EmailSender;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using AutoMapper;

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
                    _logger.LogInformation($"TotalDays: {(debt.WhenToPayback - DateTimeOffset.Now.DateTime.ToLocalTime()).TotalDays}");
                    if ((debt.WhenToPayback - DateTimeOffset.Now.DateTime.ToLocalTime()).TotalDays < 1 &&
                        (debt.WhenToPayback - DateTimeOffset.Now.DateTime.ToLocalTime()).TotalDays > 0 &&
                        !debt.EmailSended)
                    {
                        var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                        var student = await studentService.GetStudentById(debt.StudentId);

                        if (student.Email != null &&
                            emailService != null)
                        {
                            var content = GetContentMessage(isDevelopment);

                            content = content.Replace("DATETIMENOW", $"{DateTimeOffset.Now.DateTime.ToShortDateString()}")
                                             .Replace("STUDENTNAME", $"{student.FirstName ?? student.UserName}")
                                             .Replace("BORROWED", $"{debt.Borrowed}")
                                             .Replace("WHENTOPAYBACK", $"{debt.WhenToPayback.ToShortDateString()}");

                            await emailService.SendEmail(
                                new EmailModel()
                                {
                                    EmailFrom = "jozzzwardtm@mail.ru",
                                    EmailTo = student.Email,
                                    Subject = "One of your debts is about to expire",
                                    Message = content
                                });

                            debt.EmailSended = true;
                            await debtService.UpdateDebt(debt.Id, _mapper.Map<UpdateDebtModel>(debt));
                            _logger.LogInformation($"Notification about debt sent to email({student.Email}).");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error: {e.Message}");
            }
        }
    }
    private static string GetContentMessage(bool isDevelopment)
    {
        string path;
        if (isDevelopment)
            path = Directory.GetCurrentDirectory() + "\\EmailTask\\EmailContentPages\\debtNotification.html";
        else
            path = "/app/emailspages/debtNotification.html";

        string content = File.ReadAllText(path);

        return content;
    }
}
