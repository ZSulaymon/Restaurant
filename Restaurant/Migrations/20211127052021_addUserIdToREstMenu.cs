using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Migrations
{
    public partial class addUserIdToREstMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestMenu_RestInfo_RestId",
                table: "RestMenu");

            migrationBuilder.DropIndex(
                name: "IX_RestMenu_RestId",
                table: "RestMenu");

            migrationBuilder.AddColumn<Guid>(
                name: "RestInfoId",
                table: "RestMenu",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RestMenu",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestMenu_RestInfoId",
                table: "RestMenu",
                column: "RestInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_RestMenu_UserId",
                table: "RestMenu",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestMenu_RestInfo_RestInfoId",
                table: "RestMenu",
                column: "RestInfoId",
                principalTable: "RestInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RestMenu_Users_UserId",
                table: "RestMenu",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestMenu_RestInfo_RestInfoId",
                table: "RestMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RestMenu_Users_UserId",
                table: "RestMenu");

            migrationBuilder.DropIndex(
                name: "IX_RestMenu_RestInfoId",
                table: "RestMenu");

            migrationBuilder.DropIndex(
                name: "IX_RestMenu_UserId",
                table: "RestMenu");

            migrationBuilder.DropColumn(
                name: "RestInfoId",
                table: "RestMenu");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RestMenu");

            migrationBuilder.CreateIndex(
                name: "IX_RestMenu_RestId",
                table: "RestMenu",
                column: "RestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestMenu_RestInfo_RestId",
                table: "RestMenu",
                column: "RestId",
                principalTable: "RestInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
