namespace BeforeTheScholarship.Api.Configuration;

using BeforeTheScholarship.Common.Validation;
using FluentValidation.AspNetCore;

/// <summary>
/// Validator Configuration
/// </summary>
public static class ValidatorConfiguration
{

    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        ValidatorsRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }

    /// <summary>
    /// Adds validator to app
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /*public static IMvcBuilder AddValidator(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var fieldErrors = new List<ErrorResponseFieldInfo>();
                foreach (var (field, state) in context.ModelState)
                {
                    if (state.ValidationState == ModelValidationState.Invalid)
                        fieldErrors.Add(new ErrorResponseFieldInfo()
                        {
                            FieldName = field,
                            Message = string.Join(", ", state.Errors.Select(x => x.ErrorMessage))
                        });
                }

                var result = new BadRequestObjectResult(new ErrorResponse()
                {
                    ErrorCode = 100,
                    Message = "One or more validation errors occurred.",
                    FieldErrors = fieldErrors
                });

                return result;
            };
        });

        builder.AddFluentValidation(fv =>
        {
            fv.DisableDataAnnotationsValidation = true;
            fv.ImplicitlyValidateChildProperties = true;
            fv.AutomaticValidationEnabled = true;
        });

        ValidatorsRegisterHelper.Register(builder.Services);

        builder.Services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return builder;
    }*/
}
