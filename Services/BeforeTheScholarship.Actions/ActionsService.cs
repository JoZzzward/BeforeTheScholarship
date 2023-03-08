using BeforeTheScholarship.Common.Consts;
using BeforeTheScholarship.RabbitMq;
using BeforeTheScholarship.Services.EmailSender;

namespace BeforeTheScholarship.Actions;

public class ActionsService : IActionsService
{
    private readonly IRabbitMqService _rabbitMqService;

    public ActionsService(
        IRabbitMqService rabbitMqService
        )
    {
        _rabbitMqService = rabbitMqService;
    }

    public async Task SendEmail(EmailModel model, double delay)
    {
        await _rabbitMqService.PushAsync(ActionConsts.SEND_DEBT_EMAIL, model, delay);
    }
}
