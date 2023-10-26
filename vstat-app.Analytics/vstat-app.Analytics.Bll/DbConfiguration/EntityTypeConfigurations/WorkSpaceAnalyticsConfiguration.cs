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
    public class WorkSpaceAnalyticsConfiguration : IEntityTypeConfiguration<WorkSpaceAnalytics>
    {
        public void Configure(EntityTypeBuilder<WorkSpaceAnalytics> builder)
        {
            builder.HasKey(wsa => wsa.WorkSpaceId);
            builder.Property(wsa => wsa.WorkSpaceId)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id))
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(wsa => wsa.OwnerId)
                .HasConversion(
                    id => id.ToString(),
                    id => Ulid.Parse(id))
                .IsRequired();
            builder.Property(wsa => wsa.Name).IsRequired();

            builder.HasMany(wsa => wsa.Files)
                   .WithOne()
                   .HasForeignKey(fa => fa.WorkSpaceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(wsa => wsa.FileViews)
                   .WithOne()
                   .HasForeignKey(fva => fva.WorkSpaceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
