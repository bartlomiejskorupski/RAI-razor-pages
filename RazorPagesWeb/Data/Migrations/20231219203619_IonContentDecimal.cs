using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class IonContentDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Content",
                table: "Ions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Content",
                table: "Ions",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
