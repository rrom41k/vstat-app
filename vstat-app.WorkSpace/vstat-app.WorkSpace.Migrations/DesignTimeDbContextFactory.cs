namespace vstat_app.WorkSpace.Migrations;
using Bll.DbConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System.IO;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkSpaceDbContext>
{
    public WorkSpaceDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<WorkSpaceDbContext>();

        var connectionString = configuration.GetConnectionString("WorkSpace");

        builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name));

        return new WorkSpaceDbContext(builder.Options);
    }
}