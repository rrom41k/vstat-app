using Microsoft.EntityFrameworkCore;

using vstat_app.WorkSpace.Bll;
using vstat_app.WorkSpace.Bll.DbConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<WorkSpaceDbContext>(
    options =>
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("WorkSpace");

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
                Title = "WorkSpace API",
                Description = "Микросервис для:\n "
                    + "- группировки\n"
                    + "- предоставления доступа\n"
                    + "- настройки отображения\n"
                    + "- подсчета данных аналитики по Пространству\n"
            });
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<WorkSpaceService>();
builder.Services.AddScoped<WorkSpaceFileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

app.UseAuthorization();

app.MapControllers();

app.Run();
