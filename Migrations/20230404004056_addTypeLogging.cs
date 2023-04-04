using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class addTypeLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Operation",
                table: "Loggings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeLoggingId",
                table: "Loggings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeLogging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeLogging", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loggings_TypeLoggingId",
                table: "Loggings",
                column: "TypeLoggingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_TypeLogging_TypeLoggingId",
                table: "Loggings",
                column: "TypeLoggingId",
                principalTable: "TypeLogging",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_TypeLogging_TypeLoggingId",
                table: "Loggings");

            migrationBuilder.DropTable(
                name: "TypeLogging");

            migrationBuilder.DropIndex(
                name: "IX_Loggings_TypeLoggingId",
                table: "Loggings");

            migrationBuilder.DropColumn(
                name: "Operation",
                table: "Loggings");

            migrationBuilder.DropColumn(
                name: "TypeLoggingId",
                table: "Loggings");
        }
    }
}
