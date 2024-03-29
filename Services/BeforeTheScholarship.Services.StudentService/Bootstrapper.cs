﻿using Microsoft.Extensions.DependencyInjection;

namespace BeforeTheScholarship.Services.StudentService;

public static class Bootstrapper
{
    public static IServiceCollection AddStudentService(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();

        return services;
    }
}
