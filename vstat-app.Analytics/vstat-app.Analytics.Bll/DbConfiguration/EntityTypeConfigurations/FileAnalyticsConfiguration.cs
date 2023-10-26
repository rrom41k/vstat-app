using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Bll.DbConfiguration.EntityTypeConfigurations
{
    public class FileAnalyticsConfiguration : IEntityTypeConfiguration<FileAnalytics>
    {
        public void Configure(EntityTypeBuilder<FileAnalytics> builder)
        {
            builder.HasKey(fa => fa.Id);
            builder.Property(fa => fa.Id)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id))
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(fa => fa.WorkSpaceId)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id))
                .IsRequired();
            builder.Property(fa => fa.OwnerId)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id))
                .IsRequired();
            builder.Property(fa => fa.FileName).IsRequired();
            builder.Property(fa => fa.TotalPagesCount).IsRequired();
            builder.Property(fa => fa.Type);
            builder.Property(fa => fa.Link);

            builder.HasMany(fa => fa.FileViews)
                   .WithOne()
                   .HasForeignKey(fva => fva.FileId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
