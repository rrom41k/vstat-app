using Microsoft.EntityFrameworkCore;

using vstat_app.Storage.Migrations;

MigrateDatabase();

void MigrateDatabase()
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