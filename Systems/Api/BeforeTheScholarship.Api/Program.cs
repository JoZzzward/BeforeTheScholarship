using BeforeTheScholarship.Api;
using BeforeTheScholarship.Context;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAppVersioning();

services.AddAppDbContext(builder.Configuration);

services.RegisterAppServices();

services.AddAppAutoMapper();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true);

app.Run();
