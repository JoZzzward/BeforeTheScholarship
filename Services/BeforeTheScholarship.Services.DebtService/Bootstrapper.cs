using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Services.DebtService;

public static class Bootstrapper
{
    public static IServiceCollection AddDebtService(this IServiceCollection services)
    {
        services.AddScoped<IDebtService, DebtService>();

        return services;
    }
}
