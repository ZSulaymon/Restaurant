using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Migrations
{
    public partial class addRestModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestReferencePoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestAdministrator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestMenu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Composition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoocingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestMenu", x => x.id);
                    table.ForeignKey(
                        name: "FK_RestMenu_RestInfo_RestId",
                        column: x => x.RestId,
                        principalTable: "RestInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestMenu_RestId",
                table: "RestMenu",
                column: "RestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestMenu");

            migrationBuilder.DropTable(
                name: "RestInfo");
        }
    }
}
