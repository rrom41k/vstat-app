using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vstat_app.WorkSpace.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkSpace",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workspace_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    WorkSpaceId = table.Column<string>(type: "text", nullable: false),
                    StorageId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workspacefile_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "files_id_workspace_fkey",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceFile_WorkSpaceId",
                table: "WorkSpaceFile",
                column: "WorkSpaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSpaceFile");

            migrationBuilder.DropTable(
                name: "WorkSpace");
        }
    }
}
