using AutoMapper;
using BeforeTheScholarship.Common.Consts;
using BeforeTheScholarship.RabbitMq;
using BeforeTheScholarship.Services.EmailSender;

namespace BeforeTheScholarship.EmailWorker.EmailTask;

public class TaskEmailSender : ITaskEmailSender
{
    private readonly ILogger<TaskEmailSender> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly IRabbitMqService _rabbitMqService;

    public TaskEmailSender(
        ILogger<TaskEmailSender> logger,
        IServiceProvider serviceProvider,
        IMapper mapper,
        IRabbitMqService rabbitMqService
        )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
        _rabbitMqService = rabbitMqService;
    }

    public void Start()
    {
        _rabbitMqService.Subscribe<EmailModel>(ActionConsts.SEND_DEBT_EMAIL, async data
            => await Execute<IEmailSender>(async service =>
            {
                _logger.LogInformation("--> RABBITMQ: NOTIFICATION ABOUT DEBT SENT TO: {EmailTo}", data.EmailTo);
                await service.SendEmail(data);
            }));
    }

    private async Task Execute<T>(Func<T, Task> action)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();

            var service = scope.ServiceProvider.GetService<T>();
            if (service != null)
                await action(service);
            else
                _logger.LogError("Error: {Action} wasn`t resolved", action);
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {ActionConstSendDebt}: {e.Message}", ActionConsts.SEND_DEBT_EMAIL, e.Message);
            throw;
        }
    }
}
