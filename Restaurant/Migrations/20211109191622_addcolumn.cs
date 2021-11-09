using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Migrations
{
    public partial class addcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RestInfo",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestInfo_UserId",
                table: "RestInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestInfo_Users_UserId",
                table: "RestInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestInfo_Users_UserId",
                table: "RestInfo");

            migrationBuilder.DropIndex(
                name: "IX_RestInfo_UserId",
                table: "RestInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RestInfo");
        }
    }
}
