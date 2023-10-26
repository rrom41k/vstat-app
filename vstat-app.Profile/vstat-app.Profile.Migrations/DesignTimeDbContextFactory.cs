using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using vstat_app.Profile.Bll.DbConfiguration;

namespace vstat_app.Profile.Migrations;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProfileDbContext>
{
    public ProfileDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ProfileDbContext>();

        var connectionString = configuration.GetConnectionString("Profile");

        builder.UseNpgsql(
            connectionString,
            options => options.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name));

        return new(builder.Options);
    }
}