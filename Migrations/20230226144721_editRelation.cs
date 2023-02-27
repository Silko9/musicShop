using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class editRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceRecord");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ensembles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "RecordPerformance",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false),
                    PerformanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordPerformance", x => new { x.RecordId, x.PerformanceId });
                    table.ForeignKey(
                        name: "FK_RecordPerformance_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordPerformance_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordPerformance_PerformanceId",
                table: "RecordPerformance",
                column: "PerformanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordPerformance");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ensembles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PerformanceRecord",
                columns: table => new
                {
                    PerformancesId = table.Column<int>(type: "int", nullable: false),
                    RecordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceRecord", x => new { x.PerformancesId, x.RecordsId });
                    table.ForeignKey(
                        name: "FK_PerformanceRecord_Performances_PerformancesId",
                        column: x => x.PerformancesId,
                        principalTable: "Performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformanceRecord_Records_RecordsId",
                        column: x => x.RecordsId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceRecord_RecordsId",
                table: "PerformanceRecord",
                column: "RecordsId");
        }
    }
}
