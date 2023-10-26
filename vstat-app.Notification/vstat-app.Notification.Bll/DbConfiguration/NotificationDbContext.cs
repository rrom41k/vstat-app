using Microsoft.EntityFrameworkCore;

using vstat_app.Notification.Contracts.Models;

namespace vstat_app.Notification.Bll.DbConfiguration;

public sealed class NotificationDbContext : DbContext
{
    public DbSet<Notifications> Notifications { get; set; }

    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.NotificationID).HasName("NotificationsPrimaryKey");

            entity.ToTable(nameof(Notifications));

            entity.Property(e => e.UserID).IsRequired().HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).IsRequired().HasColumnName("CreatedAt");
            entity.Property(e => e.Title).IsRequired().HasColumnName("Title");
            entity.Property(e => e.Body).IsRequired().HasColumnName("Body");
            entity.Property(e => e.WasRead).IsRequired().HasColumnName("WasRead");
            entity.Property(e => e.EventType).IsRequired().HasColumnName("EventType");
        });
    }
}