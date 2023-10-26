using Microsoft.EntityFrameworkCore;

using vstat_app.WorkSpace.Contracts.Models;

namespace vstat_app.WorkSpace.Bll.DbConfiguration;

public sealed class WorkSpaceDbContext : DbContext
{
    public DbSet<Contracts.Models.WorkSpace> WorkSpaces { get; set; }
    public DbSet<WorkSpaceFile> WorkSpaceFiles { get; set; }

    public WorkSpaceDbContext(DbContextOptions<WorkSpaceDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkSpaceFile>(
            entity =>
            {
                entity.HasKey(e => e.Id).HasName("workspacefile_pkey");

                entity.ToTable("WorkSpaceFile");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.WorkSpaceId).HasColumnName("WorkSpaceId");
                entity.Property(e => e.StorageId).HasColumnName("StorageId");

                entity.HasOne(d => d.WorkSpace)
                    .WithMany(p => p.WorkSpaceFiles)
                    .HasForeignKey(d => d.WorkSpaceId)
                    .HasConstraintName("files_id_workspace_fkey");
            });
        modelBuilder.Entity<Contracts.Models.WorkSpace>(
            entity =>
            {
                entity.HasKey(e => e.Id).HasName("workspace_pkey");

                entity.ToTable("WorkSpace");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.OwnerId).HasColumnName("OwnerId");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Title).HasColumnName("Title");
                entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
            });
    }
}