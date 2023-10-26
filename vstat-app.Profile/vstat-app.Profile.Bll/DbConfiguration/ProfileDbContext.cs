using Microsoft.EntityFrameworkCore;

using vstat_app.Profile.Contracts.Models;

namespace vstat_app.Profile.Bll.DbConfiguration;

public sealed class ProfileDbContext : DbContext
{
    public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
    {
    }

    public DbSet<Contracts.Models.Profile> Profiles { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contracts.Models.Profile>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Surname).IsRequired();
                entity.Property(e => e.MiddleName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Ignore(e => e.Contacts);
                entity.HasMany(e => e.Contacts)
                    .WithOne(e => e.Profile)
                    .HasForeignKey(e => e.ProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        modelBuilder.Entity<Contact>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProfileId).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Ignore(e => e.Profile);
            });
    }
}