using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderId = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    createdOn = table.Column<DateTime>(nullable: false),
                    expectedDate = table.Column<DateTime>(nullable: false),
                    comment = table.Column<string>(nullable: true),
                    done = table.Column<DateTime>(nullable: true),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.orderId);
                });

            migrationBuilder.CreateTable(
                name: "OysterTypePrices",
                columns: table => new
                {
                    oysterType = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OysterTypePrices", x => x.oysterType);
                });

            migrationBuilder.CreateTable(
                name: "SubOrders",
                columns: table => new
                {
                    subOrderId = table.Column<Guid>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    oysterType = table.Column<int>(nullable: false),
                    orderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubOrders", x => x.subOrderId);
                    table.ForeignKey(
                        name: "FK_SubOrders_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "orderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubOrders_orderId",
                table: "SubOrders",
                column: "orderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OysterTypePrices");

            migrationBuilder.DropTable(
                name: "SubOrders");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
