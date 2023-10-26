using Microsoft.EntityFrameworkCore;

using vstat_app.Profile.Bll;
using vstat_app.Profile.Bll.DbConfiguration;
using vstat_app.Profile.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ProfileDbContext>(
    options =>
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Profile");

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
                Title = "Profile API",
                Description = "Микросервис для:"
                    + "\n- создания профиля"
                    + "\n- изменения профиля"
                    + "\n- удаления профиля"
                    + "\n- ведения списка контактов"
                    + "\n- подсчета данных по активностям, связанным с общей работой на сайте (статус: в сети, не в сети, "
                    + "сколько времени в сети), взаимодействия с другими пользователями (с каким количеством людей контактировал)"
            });
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();