using BeforeTheScholarship.Context;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAppVersioning();
services.AddAppAutoMapper();

services.AddAppDbContext(builder.Configuration);

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
