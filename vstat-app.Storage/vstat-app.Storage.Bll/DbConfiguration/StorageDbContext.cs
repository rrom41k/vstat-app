namespace vstat_app.Storage.Bll.DbConfiguration;

using Microsoft.EntityFrameworkCore;

using Contracts.Models;

public sealed class StorageDbContext : DbContext
{
    public StorageDbContext(DbContextOptions<StorageDbContext> options)
        : base(options)
    {
    }

    public DbSet<File> Files { get; set; }
    public DbSet<Storage> Storages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("storage_pkey");

            entity.ToTable("Storage");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.UserId).HasColumnName("UserId");
        });
        
        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("files_pkey");

            entity.ToTable("Files");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerId");
            entity.Property(e => e.Name).HasColumnName("Name");
            entity.Property(e => e.Extension).HasColumnName("Extension");
            entity.Property(e => e.BucketId).HasColumnName("BucketId");
            entity.Property(e => e.StorageId).HasColumnName("StorageId");

            entity.HasOne(d => d.Storage).WithMany(p => p.Files)
                .HasForeignKey(d => d.StorageId)
                .HasConstraintName("files_id_storage_fkey");
        });
    }
}