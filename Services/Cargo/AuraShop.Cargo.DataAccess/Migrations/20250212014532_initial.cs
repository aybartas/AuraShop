using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuraShop.Cargo.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingNumber = table.Column<int>(type: "int", nullable: false),
                    CargoCompanyId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargo_CargoCompany_CargoCompanyId",
                        column: x => x.CargoCompanyId,
                        principalTable: "CargoCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoAction_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CargoCompany",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "DHL" });

            migrationBuilder.InsertData(
                table: "CargoCompany",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "FedEx" });

            migrationBuilder.InsertData(
                table: "CargoCompany",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "UPS" });

            migrationBuilder.InsertData(
                table: "Cargo",
                columns: new[] { "Id", "CargoCompanyId", "DeliveredDate", "EstimatedDeliveryDate", "OrderNumber", "ShippedDate", "Status", "TrackingNumber" },
                values: new object[] { 1, 1, null, new DateTime(2025, 2, 14, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8252), "ORD123456", new DateTime(2025, 2, 9, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8248), "InTransit", 10001 });

            migrationBuilder.InsertData(
                table: "Cargo",
                columns: new[] { "Id", "CargoCompanyId", "DeliveredDate", "EstimatedDeliveryDate", "OrderNumber", "ShippedDate", "Status", "TrackingNumber" },
                values: new object[] { 2, 2, new DateTime(2025, 2, 11, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8258), new DateTime(2025, 2, 11, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8258), "ORD123457", new DateTime(2025, 2, 7, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8258), "Delivered", 10002 });

            migrationBuilder.InsertData(
                table: "CargoAction",
                columns: new[] { "Id", "ActionDate", "CargoId", "Message" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 9, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8265), 1, "Package picked up." },
                    { 2, new DateTime(2025, 2, 10, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8266), 1, "Package in transit." },
                    { 3, new DateTime(2025, 2, 7, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8267), 2, "Package picked up." },
                    { 4, new DateTime(2025, 2, 11, 1, 45, 32, 371, DateTimeKind.Utc).AddTicks(8267), 2, "Package delivered." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_CargoCompanyId",
                table: "Cargo",
                column: "CargoCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAction_CargoId",
                table: "CargoAction",
                column: "CargoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoAction");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "CargoCompany");
        }
    }
}
