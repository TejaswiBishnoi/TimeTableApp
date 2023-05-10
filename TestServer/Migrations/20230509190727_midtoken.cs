using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestServer.Migrations
{
    public partial class midtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoogleMiddleTokens",
                columns: table => new
                {
                    token = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    instructor_id = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleMiddleTokens", x => x.token);
                    table.ForeignKey(
                        name: "FK_GoogleMiddleTokens_Instructors_instructor_id",
                        column: x => x.instructor_id,
                        principalTable: "Instructors",
                        principalColumn: "instructor_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GoogleMiddleTokens_instructor_id",
                table: "GoogleMiddleTokens",
                column: "instructor_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoogleMiddleTokens");
        }
    }
}
