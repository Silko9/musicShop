using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class updateMusicianViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ensembles_TypeEnsembles_TypeEnsembleId",
                table: "Ensembles");

            migrationBuilder.AlterColumn<int>(
                name: "TypeEnsembleId",
                table: "Ensembles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ensembles_TypeEnsembles_TypeEnsembleId",
                table: "Ensembles",
                column: "TypeEnsembleId",
                principalTable: "TypeEnsembles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ensembles_TypeEnsembles_TypeEnsembleId",
                table: "Ensembles");

            migrationBuilder.AlterColumn<int>(
                name: "TypeEnsembleId",
                table: "Ensembles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ensembles_TypeEnsembles_TypeEnsembleId",
                table: "Ensembles",
                column: "TypeEnsembleId",
                principalTable: "TypeEnsembles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
