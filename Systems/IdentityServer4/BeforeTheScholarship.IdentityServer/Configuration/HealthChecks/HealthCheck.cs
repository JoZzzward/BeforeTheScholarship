﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace BeforeTheScholarship.IdentityServer.Configuration.HealthChecks;

public class HealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("BeforeTheScholarship.IdentityServer");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
}