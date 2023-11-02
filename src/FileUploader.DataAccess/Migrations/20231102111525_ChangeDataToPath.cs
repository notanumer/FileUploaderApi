using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploader.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataToPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Files",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Files");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Files",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
