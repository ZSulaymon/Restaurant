using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestMenu_FoodCatigories_CategoryId",
                table: "RestMenu");

            migrationBuilder.DropIndex(
                name: "IX_RestMenu_CategoryId",
                table: "RestMenu");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodCategoryId",
                table: "RestMenu",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestMenu_FoodCategoryId",
                table: "RestMenu",
                column: "FoodCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestMenu_FoodCatigories_FoodCategoryId",
                table: "RestMenu",
                column: "FoodCategoryId",
                principalTable: "FoodCatigories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestMenu_FoodCatigories_FoodCategoryId",
                table: "RestMenu");

            migrationBuilder.DropIndex(
                name: "IX_RestMenu_FoodCategoryId",
                table: "RestMenu");

            migrationBuilder.DropColumn(
                name: "FoodCategoryId",
                table: "RestMenu");

            migrationBuilder.CreateIndex(
                name: "IX_RestMenu_CategoryId",
                table: "RestMenu",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestMenu_FoodCatigories_CategoryId",
                table: "RestMenu",
                column: "CategoryId",
                principalTable: "FoodCatigories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
