using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using vstat_app.Notification.Bll.DbConfiguration;


namespace vstat_app.Notification.Migrations;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NotificationDbContext>
{
    public NotificationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<NotificationDbContext>();

        var connectionString = configuration.GetConnectionString("Notification");

        builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name));

        return new NotificationDbContext(builder.Options);
    }
}