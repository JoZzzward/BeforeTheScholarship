﻿using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.DebtService;
using BeforeTheScholarship.StudentService;
using BeforeTheScholarship.EmailWorker.EmailTask;
using BeforeTheScholarship.Api.Configuration;

namespace BeforeTheScholarship.EmailWorker;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddDebtService()
            .AddStudentService()
            .AddEmailSender()
            .AddAppAutoMapper()
            ;

        services.AddSingleton<ITaskEmailSender, TaskEmailSender>();

        return services;
    }
}
