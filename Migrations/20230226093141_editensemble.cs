using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class editensemble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicians_Ensembles_EnsembleId",
                table: "Musicians");

            migrationBuilder.DropIndex(
                name: "IX_Musicians_EnsembleId",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "EnsembleId",
                table: "Musicians");

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

            migrationBuilder.CreateIndex(
                name: "IX_MusicianEnsemble_EnsembleId",
                table: "MusicianEnsemble",
                column: "EnsembleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicianEnsemble");

            migrationBuilder.AddColumn<int>(
                name: "EnsembleId",
                table: "Musicians",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_EnsembleId",
                table: "Musicians",
                column: "EnsembleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicians_Ensembles_EnsembleId",
                table: "Musicians",
                column: "EnsembleId",
                principalTable: "Ensembles",
                principalColumn: "Id");
        }
    }
}
