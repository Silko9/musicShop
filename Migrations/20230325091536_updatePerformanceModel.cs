using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class updatePerformanceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CircumstancesExecution",
                table: "Performances",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CircumstancesExecution",
                table: "Performances");
        }
    }
}
