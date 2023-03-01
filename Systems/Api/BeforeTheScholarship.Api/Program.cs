using BeforeTheScholarship.Api;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Services.Settings;
using BeforeTheScholarship.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

var identitySettings = Settings.Load<IdentitySettings>("IdentitySettings");

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppVersioning();

services.AddAppAuth(identitySettings);
services.AddAppSwagger(identitySettings);

services.RegisterAppServices();

services.AddAppAutoMapper();

var app = builder.Build();

app.UseAppCors();

app.UseHealthChecks();

app.UseAppAuth();
app.UseAppSwagger();

app.MapControllers();

DbInitializer.Execute(app.Services);

app.Run();
