using Microsoft.EntityFrameworkCore;

using vstat_app.Notification.Bll;
using vstat_app.Notification.Bll.DbConfiguration;
using vstat_app.Notification.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NotificationDbContext>(options =>
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    var connectionString = configuration.GetConnectionString("Notification");

    options.UseNpgsql(connectionString);
});

builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc(
            "v1",
            new()
            {
                Version = "v1",
                Title = "Notification API",
                Description = "Микросервис для:" +
                    "\n- получения информации о событиях в системе" +
                    "\n- генерации уведомления" +
                    "\n- отправки уведомления во внешние системы доставок" +
                    "\n- настройки источников уведомлений"
            });
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        ;
    });


app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
