using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IlgiluftaExam.Migrations
{
    public partial class First3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enthusiasts",
                columns: table => new
                {
                    EnthusiastID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Proficiency = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HobbyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enthusiasts", x => x.EnthusiastID);
                    table.ForeignKey(
                        name: "FK_Enthusiasts_Hobbys_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "Hobbys",
                        principalColumn: "HobbyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enthusiasts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Enthusiasts_HobbyId",
                table: "Enthusiasts",
                column: "HobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_Enthusiasts_UserId",
                table: "Enthusiasts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enthusiasts");
        }
    }
}
