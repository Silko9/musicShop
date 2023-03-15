using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class addMusicianDetailsViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicianEnsemble");

            migrationBuilder.DropTable(
                name: "MusicianRoles");

            migrationBuilder.DropTable(
                name: "RecordPerformance");

            migrationBuilder.CreateTable(
                name: "EnsembleMusician",
                columns: table => new
                {
                    EnsemblesId = table.Column<int>(type: "int", nullable: false),
                    MusiciansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnsembleMusician", x => new { x.EnsemblesId, x.MusiciansId });
                    table.ForeignKey(
                        name: "FK_EnsembleMusician_Ensembles_EnsemblesId",
                        column: x => x.EnsemblesId,
                        principalTable: "Ensembles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnsembleMusician_Musicians_MusiciansId",
                        column: x => x.MusiciansId,
                        principalTable: "Musicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicianRole",
                columns: table => new
                {
                    MusiciansId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianRole", x => new { x.MusiciansId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_MusicianRole_Musicians_MusiciansId",
                        column: x => x.MusiciansId,
                        principalTable: "Musicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_EnsembleMusician_MusiciansId",
                table: "EnsembleMusician",
                column: "MusiciansId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianRole_RolesId",
                table: "MusicianRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceRecord_RecordsId",
                table: "PerformanceRecord",
                column: "RecordsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnsembleMusician");

            migrationBuilder.DropTable(
                name: "MusicianRole");

            migrationBuilder.DropTable(
                name: "PerformanceRecord");

            migrationBuilder.CreateTable(
                name: "MusicianEnsemble",
                columns: table => new
                {
                    MusicianId = table.Column<int>(type: "int", nullable: false),
                    EnsembleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianEnsemble", x => new { x.MusicianId, x.EnsembleId });
                    table.ForeignKey(
                        name: "FK_MusicianEnsemble_Ensembles_EnsembleId",
                        column: x => x.EnsembleId,
                        principalTable: "Ensembles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianEnsemble_Musicians_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicianRoles",
                columns: table => new
                {
                    MusicianId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianRoles", x => new { x.MusicianId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MusicianRoles_Musicians_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_MusicianEnsemble_EnsembleId",
                table: "MusicianEnsemble",
                column: "EnsembleId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianRoles_RoleId",
                table: "MusicianRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordPerformance_PerformanceId",
                table: "RecordPerformance",
                column: "PerformanceId");
        }
    }
}
