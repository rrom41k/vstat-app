using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vstat_app.Analytics.Contracts.Models;

namespace vstat_app.Analytics.Bll.DbConfiguration.EntityTypeConfigurations
{
    public class FileViewAnalyticsConfiguration : IEntityTypeConfiguration<FileViewAnalytics>
    {
        public void Configure(EntityTypeBuilder<FileViewAnalytics> builder)
        {
            builder.HasKey(fva => new { fva.WorkSpaceId, fva.FileId, fva.ViewerId });
            builder.Property(fva => fva.WorkSpaceId)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id))
                .IsRequired();
            builder.Property(fva => fva.FileId)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id))
                .IsRequired();
            builder.Property(fva => fva.ViewerId)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id));
            builder.Property(fva => fva.ViewCount);
            builder.Property(fva => fva.ViewTime);
            builder.Property(fva => fva.ViewDate);
        }
    }
}
