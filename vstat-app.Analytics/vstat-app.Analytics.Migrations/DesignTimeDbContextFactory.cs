using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using vstat_app.Analytics.Bll.DbConfiguration;

namespace vstat_app.Analytics.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AnalyticsDbContext>
    {
        public AnalyticsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AnalyticsDbContext>();
            var connectionString = configuration.GetConnectionString("Analytics");
            builder.UseNpgsql(connectionString, options => options.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name));

            return new AnalyticsDbContext(builder.Options);
        }
    }
}
