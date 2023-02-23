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
services.AddAppAuth(identitySettings);

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger(identitySettings);
services.AddAppAutoMapper();

services.RegisterAppServices();

var app = builder.Build();

app.UseAppCors();

app.UseHealthChecks();

app.UseAppSwagger();

app.UseAppAuth();

app.MapControllers();

//DbInitializer.Execute(app.Services);
//DbSeeder.Execute(app.Services, true);

app.Run();
