using Carter;
using GoTask.API.Handlers;
using GoTask.Application;
using GoTask.Infrastructure.Extensions;
using GoTask.Infrastructure.Migrations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCarter();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();
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