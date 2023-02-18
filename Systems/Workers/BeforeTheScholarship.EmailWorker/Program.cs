using BeforeTheScholarship.Context;
using BeforeTheScholarship.EmailWorker;
using BeforeTheScholarship.EmailWorker.Configuration;
using BeforeTheScholarship.EmailWorker.EmailTask;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppHealthChecks();

services.AddAppDbContext(builder.Configuration);

services.RegisterAppServices();
var app = builder.Build();

app.UseHealthChecks();

app.Services.GetRequiredService<ITaskEmailSender>().Start(builder.Environment.IsDevelopment());

app.Run();