using Carter;
using GoTask.Infrastructure.Extensions;
using GoTask.Infrastructure.Migrations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapCarter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

if (!builder.Configuration.IsTestEnvironment())
    await MigrateDatabaseAsync();

app.Run();

return;

async Task MigrateDatabaseAsync()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DataBaseMigrations.MigrateDatabaseAsync(scope.ServiceProvider);
}