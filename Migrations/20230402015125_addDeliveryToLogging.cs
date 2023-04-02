using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class addDeliveryToLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Provider_ProviderId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provider",
                table: "Provider");

            migrationBuilder.RenameTable(
                name: "Provider",
                newName: "Providers");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryId",
                table: "Loggings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Providers",
                table: "Providers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Providers_ProviderId",
                table: "Deliveries",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Providers_ProviderId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Providers",
                table: "Providers");

            migrationBuilder.RenameTable(
                name: "Providers",
                newName: "Provider");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryId",
                table: "Loggings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provider",
                table: "Provider",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Provider_ProviderId",
                table: "Deliveries",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");
        }
    }
}
