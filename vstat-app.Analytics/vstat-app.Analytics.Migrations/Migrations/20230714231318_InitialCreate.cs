using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vstat_app.Analytics.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkSpacesAnalytics",
                columns: table => new
                {
                    WorkSpaceId = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpacesAnalytics", x => x.WorkSpaceId);
                });

            migrationBuilder.CreateTable(
                name: "FilesAnalytics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    WorkSpaceId = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    TotalPagesCount = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesAnalytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesAnalytics_WorkSpacesAnalytics_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpacesAnalytics",
                        principalColumn: "WorkSpaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilesViewsAnalytics",
                columns: table => new
                {
                    WorkSpaceId = table.Column<string>(type: "text", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: false),
                    ViewerId = table.Column<string>(type: "text", nullable: false),
                    ViewCount = table.Column<int>(type: "integer", nullable: true),
                    ViewTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    ViewDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesViewsAnalytics", x => new { x.WorkSpaceId, x.FileId, x.ViewerId });
                    table.ForeignKey(
                        name: "FK_FilesViewsAnalytics_FilesAnalytics_FileId",
                        column: x => x.FileId,
                        principalTable: "FilesAnalytics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilesViewsAnalytics_WorkSpacesAnalytics_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpacesAnalytics",
                        principalColumn: "WorkSpaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesAnalytics_WorkSpaceId",
                table: "FilesAnalytics",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesViewsAnalytics_FileId",
                table: "FilesViewsAnalytics",
                column: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesViewsAnalytics");

            migrationBuilder.DropTable(
                name: "FilesAnalytics");

            migrationBuilder.DropTable(
                name: "WorkSpacesAnalytics");
        }
    }
}
