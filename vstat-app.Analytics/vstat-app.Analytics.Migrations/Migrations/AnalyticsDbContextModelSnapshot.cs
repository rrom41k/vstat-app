// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using vstat_app.Analytics.Bll.DbConfiguration;

#nullable disable

namespace vstat_app.Analytics.Migrations.Migrations
{
    [DbContext(typeof(AnalyticsDbContext))]
    partial class AnalyticsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("vstat_app.Analytics.Contracts.Models.FileAnalytics", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalPagesCount")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("WorkSpaceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("FilesAnalytics");
                });

            modelBuilder.Entity("vstat_app.Analytics.Contracts.Models.FileViewAnalytics", b =>
                {
                    b.Property<string>("WorkSpaceId")
                        .HasColumnType("text");

                    b.Property<string>("FileId")
                        .HasColumnType("text");

                    b.Property<string>("ViewerId")
                        .HasColumnType("text");

                    b.Property<int?>("ViewCount")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ViewDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan?>("ViewTime")
                        .HasColumnType("interval");

                    b.HasKey("WorkSpaceId", "FileId", "ViewerId");

                    b.HasIndex("FileId");

                    b.ToTable("FilesViewsAnalytics");
                });

            modelBuilder.Entity("vstat_app.Analytics.Contracts.Models.WorkSpaceAnalytics", b =>
                {
                    b.Property<string>("WorkSpaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WorkSpaceId");

                    b.ToTable("WorkSpacesAnalytics");
                });

            modelBuilder.Entity("vstat_app.Analytics.Contracts.Models.FileAnalytics", b =>
                {
                    b.HasOne("vstat_app.Analytics.Contracts.Models.WorkSpaceAnalytics", null)
                        .WithMany("Files")
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("vstat_app.Analytics.Contracts.Models.FileViewAnalytics", b =>
                {
                    b.HasOne("vstat_app.Analytics.Contracts.Models.FileAnalytics", null)
                        .WithMany("FileViews")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vstat_app.Analytics.Contracts.Models.WorkSpaceAnalytics", null)
                        .WithMany("FileViews")
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("vstat_app.Analytics.Contracts.Models.FileAnalytics", b =>
                {
                    b.Navigation("FileViews");
                });

            modelBuilder.Entity("vstat_app.Analytics.Contracts.Models.WorkSpaceAnalytics", b =>
                {
                    b.Navigation("FileViews");

                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}