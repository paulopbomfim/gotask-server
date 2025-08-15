using System.Text;
using Carter;
using GoTask.API.Handlers;
using GoTask.API.Middlewares;
using GoTask.Application;
using GoTask.Infrastructure.Extensions;
using GoTask.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCarter();
builder.Services.AddOpenApi();

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");

builder.Services
    .AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = new TimeSpan(0),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("org_admin", policy => policy.RequireRole("Admin"))
    .AddFallbackPolicy("RequireAuth", policy => policy.RequireAuthenticatedUser().Build());

var app = builder.Build();

app.UseExceptionHandler();
app.MapCarter();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.MapOpenApi().AllowAnonymous();
    app.MapScalarApiReference().AllowAnonymous();
}

app.UseHttpsRedirection();

if (!builder.Configuration.IsTestEnvironment())
    await MigrateDatabaseAsync();

app.UseMiddleware<TokenInfoMiddleware>();

app.Run();

return;

async Task MigrateDatabaseAsync()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DataBaseMigrations.MigrateDatabaseAsync(scope.ServiceProvider);
}