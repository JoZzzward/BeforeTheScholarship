using BeforeTheScholarship.Common.Consts;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.RabbitMqService;

namespace BeforeTheScholarship.EmailWorker.EmailTask;

public class TaskEmailSender : ITaskEmailSender
{
    private readonly ILogger<TaskEmailSender> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDebtService _debtService;
    private readonly IRabbitMqService _rabbitMqService;

    public TaskEmailSender(
        ILogger<TaskEmailSender> logger,
        IServiceProvider serviceProvider,
        IDebtService debtService,
        IRabbitMqService rabbitMqService
        )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _debtService = debtService;
        _rabbitMqService = rabbitMqService;
    }

    public void Start()
    {
        _rabbitMqService.Subscribe<DebtEmailModel>(ActionConsts.SEND_DEBT_EMAIL, async data
            => await Execute<IEmailSender>(async service =>
            {
                _logger.LogInformation("--> RABBITMQ: TRYING TO SEND NOTIFICATION ABOUT DEBT TO: {EmailTo}", data.EmailTo);

                var debts = await _debtService.GetDebts();

                var debt = debts.FirstOrDefault(x => x.WhenToPayback.ToShortStringFormat() == data.WhenToPayback.ToShortStringFormat());
                
                if (debt == null)
                {
                    _logger.LogInformation("--> The notification of the debt to be repaid with one day was not sent. Debt with specified data was not found ({DataTime})", data.WhenToPayback.DateTime.ToLocalTime().ToString("MM/dd/yy H:mm"));
                    return;
                }
                    
                await service.SendDebtEmail(data);

                _logger.LogInformation("--> RABBITMQ: NOTIFICATION ABOUT DEBT SENT TO: {EmailTo}", data.EmailTo);
            }));
    }

    private async Task Execute<T>(Func<T, Task> action)
    {
        using var scope = _serviceProvider.CreateScope();

        var service = scope.ServiceProvider.GetService<T>();

        if (service == null)
        {
            _logger.LogError("Error: {Service} not found", service);

            throw new NullReferenceException($"Specified service <{typeof(T)}> not found");
        }

        await action(service);
    }
}
