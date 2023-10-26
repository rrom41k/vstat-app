using Microsoft.EntityFrameworkCore;

using vstat_app.Storage.Bll;
using vstat_app.Storage.Bll.DbConfiguration;
using vstat_app.Storage.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<StorageDbContext>(options =>
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    var connectionString = configuration.GetConnectionString("Storage");

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
                Title = "Storage API",
                Description = "Микросервис для:\n "
                    + "- взаимодействия с внешним сервисом хранения данных (aws, minio)\n"
                    + "- выделения места\n"
                    + "- управления данными в хранилище\n"
                    + "- ведения аналитики по количеству обращений к файлам\n"
                    + "- проверки доступного места в хранилище"
            });
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<FileService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        ;
    });

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/


app.UseAuthorization();

app.MapControllers();

app.Run();
