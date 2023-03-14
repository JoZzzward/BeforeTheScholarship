using BeforeTheScholarship.Common.Consts;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.RabbitMqService;

namespace BeforeTheScholarship.Services.Actions;

public class ActionsService : IActionsService
{
    private readonly IRabbitMqService _rabbitMqService;

    public ActionsService(
        IRabbitMqService rabbitMqService
        )
    {
        _rabbitMqService = rabbitMqService;
    }

    public async Task SendDebtEmail(DebtEmailModel model, double delay)
    {
        await _rabbitMqService.PushAsync(ActionConsts.SEND_DEBT_EMAIL, model, delay);
    }
}
