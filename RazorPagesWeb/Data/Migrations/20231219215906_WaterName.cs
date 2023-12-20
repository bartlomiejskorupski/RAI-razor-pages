using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class WaterName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Waters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Waters");
        }
    }
}
