using BeforeTheScholarship.Context;
using BeforeTheScholarship.IdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;    

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppCors();

services.AddAppHealthChecks();

services.AddIS4();

var app = builder.Build();

app.UseAppHealthChecks();

app.UseIS4();

app.Run();

public partial class Program { }
