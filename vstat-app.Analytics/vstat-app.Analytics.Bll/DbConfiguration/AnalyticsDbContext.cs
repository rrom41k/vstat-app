using Microsoft.EntityFrameworkCore;
using vstat_app.Analytics.Bll.DbConfiguration.EntityTypeConfigurations;
using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Bll.DbConfiguration
{
    public class AnalyticsDbContext: DbContext
    {
        public DbSet<FileAnalytics> FilesAnalytics { get; set; }
        public DbSet<FileViewAnalytics> FilesViewsAnalytics { get; set; }
        public DbSet<WorkSpaceAnalytics> WorkSpacesAnalytics { get; set; }
        public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FileAnalyticsConfiguration());
            builder.ApplyConfiguration(new FileViewAnalyticsConfiguration());
            builder.ApplyConfiguration(new  WorkSpaceAnalyticsConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
