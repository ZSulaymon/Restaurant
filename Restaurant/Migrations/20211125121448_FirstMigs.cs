using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Migrations
{
    public partial class FirstMigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_RestMenu_RestMenuId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_RestMenuId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "RestMenuId",
                table: "OrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MenuId",
                table: "OrderDetails",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_RestMenu_MenuId",
                table: "OrderDetails",
                column: "MenuId",
                principalTable: "RestMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_RestMenu_MenuId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_MenuId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "RestMenuId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_RestMenuId",
                table: "OrderDetails",
                column: "RestMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_RestMenu_RestMenuId",
                table: "OrderDetails",
                column: "RestMenuId",
                principalTable: "RestMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
