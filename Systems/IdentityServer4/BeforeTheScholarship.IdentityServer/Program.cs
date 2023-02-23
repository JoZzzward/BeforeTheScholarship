using BeforeTheScholarship.Context;
using BeforeTheScholarship.IdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();

services.AddIS4();

var app = builder.Build();

app.UseIS4();

app.Run();
