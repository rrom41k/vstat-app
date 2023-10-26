using Microsoft.EntityFrameworkCore;

namespace vstat_app.Profile.Migrations;

public static class Program
{
    public static void Main(string[] args)
    {
        MigrateDatabase(args);
    }

    private static void MigrateDatabase(string[] args)
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
}