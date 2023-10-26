using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using vstat_app.Analytics.Bll.DbConfiguration;
using vstat_app.Analytics.Bll.Services;
using vstat_app.Analytics.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AnalyticsDbContext>(options =>
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    var connectionString = configuration.GetConnectionString("Analytics");

    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IFileAnalyticsService, FileAnalyticsService>();
builder.Services.AddScoped<IFileViewAnalyticsService, FileViewAnalyticsService>();
builder.Services.AddScoped<IWorkSpaceAnalyticsService, WorkSpaceAnalyticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Analytics API",
        Description = "Микросервис для:<br>" +
        "- агрегация аналитических данных из других систем<br>" +
        "- сбор данных<br>" +
        "- формирование отчета во внешней системе"
    });
});

var app = builder.Build();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); endpoints.MapSwagger(); });

app.Run();