using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Migrations
{
    public partial class AddOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_shopCartItems",
                table: "shopCartItems");

            migrationBuilder.RenameTable(
                name: "shopCartItems",
                newName: "ShopCartItems");

            migrationBuilder.AddColumn<Guid>(
                name: "RestMenuId",
                table: "ShopCartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopCartItems",
                table: "ShopCartItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    RestMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ordersid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_Ordersid",
                        column: x => x.Ordersid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_RestMenu_RestMenuId",
                        column: x => x.RestMenuId,
                        principalTable: "RestMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopCartItems_RestMenuId",
                table: "ShopCartItems",
                column: "RestMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Ordersid",
                table: "OrderDetails",
                column: "Ordersid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_RestMenuId",
                table: "OrderDetails",
                column: "RestMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItems_RestMenu_RestMenuId",
                table: "ShopCartItems",
                column: "RestMenuId",
                principalTable: "RestMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItems_RestMenu_RestMenuId",
                table: "ShopCartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopCartItems",
                table: "ShopCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShopCartItems_RestMenuId",
                table: "ShopCartItems");

            migrationBuilder.DropColumn(
                name: "RestMenuId",
                table: "ShopCartItems");

            migrationBuilder.RenameTable(
                name: "ShopCartItems",
                newName: "shopCartItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shopCartItems",
                table: "shopCartItems",
                column: "Id");
        }
    }
}
