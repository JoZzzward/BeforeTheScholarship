using BeforeTheScholarship.Api;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Services.Settings;
using BeforeTheScholarship.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

var identitySettings = AppSettings.Load<IdentitySettings>("IdentitySettings");

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppVersioning();

services.AddAppAuth(identitySettings!);
services.AddAppSwagger(identitySettings!);

services.RegisterAppServices();

services.AddAppAutoMapper();

services.AddControllersAndViews();

services.AddValidator();

var app = builder.Build();

app.UseAppCors();

app.UseHealthChecks();

app.UseAppAuth();
app.UseAppSwagger();

app.UseControllersAndViews();

DbInitializer.Execute(app.Services);

app.Run();

public partial class Program { }
