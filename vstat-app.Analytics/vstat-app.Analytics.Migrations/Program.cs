using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using vstat_app.Analytics.Bll.DbConfiguration;
using vstat_app.Analytics.Migrations;

MigrateDatabase(args);

void MigrateDatabase(string[] args)
{
    try
    {
        Console.WriteLine("Applying migrations...");
        using (var context = new DesignTimeDbContextFactory().CreateDbContext(args))
        {
            context.Database.Migrate();
        }
        Console.WriteLine("Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error applying migrations: " + ex.Message);
        throw;
    }
}