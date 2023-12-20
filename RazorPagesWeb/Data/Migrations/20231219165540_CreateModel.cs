using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<decimal>(type: "decimal(6,3)", precision: 6, scale: 3, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packagings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packagings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    ManufacturerID = table.Column<int>(type: "int", nullable: false),
                    pH = table.Column<double>(type: "float", nullable: false),
                    PackagingID = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waters_Companies_ManufacturerID",
                        column: x => x.ManufacturerID,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Waters_Packagings_PackagingID",
                        column: x => x.PackagingID,
                        principalTable: "Packagings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Waters_WaterTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "WaterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IonWater",
                columns: table => new
                {
                    IonsId = table.Column<int>(type: "int", nullable: false),
                    WatersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IonWater", x => new { x.IonsId, x.WatersId });
                    table.ForeignKey(
                        name: "FK_IonWater_Ions_IonsId",
                        column: x => x.IonsId,
                        principalTable: "Ions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IonWater_Waters_WatersId",
                        column: x => x.WatersId,
                        principalTable: "Waters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IonWater_WatersId",
                table: "IonWater",
                column: "WatersId");

            migrationBuilder.CreateIndex(
                name: "IX_Waters_ManufacturerID",
                table: "Waters",
                column: "ManufacturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Waters_PackagingID",
                table: "Waters",
                column: "PackagingID");

            migrationBuilder.CreateIndex(
                name: "IX_Waters_TypeID",
                table: "Waters",
                column: "TypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IonWater");

            migrationBuilder.DropTable(
                name: "Ions");

            migrationBuilder.DropTable(
                name: "Waters");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Packagings");

            migrationBuilder.DropTable(
                name: "WaterTypes");
        }
    }
}
