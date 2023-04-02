using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class updateLoggingRemoveNotNullToOrderAndDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings");

            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_Orders_OrderId",
                table: "Loggings");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Loggings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryId",
                table: "Loggings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_Orders_OrderId",
                table: "Loggings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings");

            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_Orders_OrderId",
                table: "Loggings");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Loggings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryId",
                table: "Loggings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_Orders_OrderId",
                table: "Loggings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
