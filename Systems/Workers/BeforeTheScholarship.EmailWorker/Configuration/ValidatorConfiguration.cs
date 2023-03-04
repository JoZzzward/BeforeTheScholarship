namespace BeforeTheScholarship.EmailWorker.Configuration;

using BeforeTheScholarship.Common.Validation;
using FluentValidation.AspNetCore;

/// <summary>
/// Validator Configuration
/// </summary>
public static class ValidatorConfiguration
{
    /// <summary>
    /// Adds validator to app
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        ValidatorsRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }
}