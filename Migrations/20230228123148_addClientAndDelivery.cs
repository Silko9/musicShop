using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicShop.Migrations
{
    public partial class addClientAndDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_TypeLoggings_TypeLoggingId",
                table: "Loggings");

            migrationBuilder.DropTable(
                name: "TypeLoggings");

            migrationBuilder.DropIndex(
                name: "IX_Loggings_TypeLoggingId",
                table: "Loggings");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Loggings");

            migrationBuilder.DropColumn(
                name: "TypeLoggingId",
                table: "Loggings");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Loggings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LegalAddress = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TIN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDelivery = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loggings_DeliveryId",
                table: "Loggings",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ProviderId",
                table: "Deliveries",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loggings_Deliveries_DeliveryId",
                table: "Loggings");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_Loggings_DeliveryId",
                table: "Loggings");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Loggings");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Loggings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TypeLoggingId",
                table: "Loggings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeLoggings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeLoggings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loggings_TypeLoggingId",
                table: "Loggings",
                column: "TypeLoggingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loggings_TypeLoggings_TypeLoggingId",
                table: "Loggings",
                column: "TypeLoggingId",
                principalTable: "TypeLoggings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
