using Microsoft.EntityFrameworkCore;
using Server;
using Server.Data;
using Server.Services;
using Shared;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISensorService, SensorService>();
builder.Services.AddTransient<IModuleService, ModuleService>();

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddDbContext<AppDbContext>(
                    options => options.UseNpgsql(Environment.GetEnvironmentVariable("LIGPE_DATABASE"))
);

Configurations.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
Configurations.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;

builder.Services.AddCors(options =>
{
    options.AddPolicy(ApiConfiguration.CorsPolicyName, policy =>
    {
        policy
            .WithOrigins("http://192.168.0.18:5236", "https://localhost:7072")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseAuthorization();

app.MapControllers();

app.Run();
