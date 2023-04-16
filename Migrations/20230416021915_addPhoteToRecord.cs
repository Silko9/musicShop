using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class addPhoteToRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phote",
                table: "Records",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phote",
                table: "Records");
        }
    }
}
