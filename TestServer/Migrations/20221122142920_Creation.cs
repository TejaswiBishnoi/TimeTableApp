using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestServer.Migrations
{
    public partial class Creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    instructor_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    department = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.instructor_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    room_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    building = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.room_code);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    course_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    course_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_credits = table.Column<decimal>(type: "decimal(2,1)", nullable: false, defaultValue: 0m),
                    lecture_credits = table.Column<decimal>(type: "decimal(2,1)", nullable: false, defaultValue: 0m),
                    tutorial_credits = table.Column<decimal>(type: "decimal(2,1)", nullable: false, defaultValue: 0m),
                    practical_credits = table.Column<decimal>(type: "decimal(2,1)", nullable: false, defaultValue: 0m),
                    coordinator_id = table.Column<string>(type: "varchar(255)", nullable: false, defaultValue: "ProfX")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    department = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.course_code);
                    table.ForeignKey(
                        name: "FK_Courses_Instructors_coordinator_id",
                        column: x => x.coordinator_id,
                        principalTable: "Instructors",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    event_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_course = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    ignore_holiday = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    owner = table.Column<string>(type: "varchar(255)", nullable: false, defaultValue: "ProfX")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_Events_Instructors_owner",
                        column: x => x.owner,
                        principalTable: "Instructors",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instrucor_Ofs",
                columns: table => new
                {
                    instrucor_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    course_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrucor_Ofs", x => new { x.course_code, x.instrucor_id });
                    table.ForeignKey(
                        name: "FK_Instrucor_Ofs_Courses_course_code",
                        column: x => x.course_code,
                        principalTable: "Courses",
                        principalColumn: "course_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instrucor_Ofs_Instructors_instrucor_id",
                        column: x => x.instrucor_id,
                        principalTable: "Instructors",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Occurences",
                columns: table => new
                {
                    occurence_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    event_id = table.Column<int>(type: "int", nullable: false),
                    time_begin = table.Column<TimeSpan>(type: "time", nullable: false),
                    time_end = table.Column<TimeSpan>(type: "time", nullable: false),
                    day = table.Column<int>(type: "int", nullable: false),
                    date_start = table.Column<DateTime>(type: "date", nullable: false),
                    date_end = table.Column<DateTime>(type: "date", nullable: false),
                    room_code = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occurences", x => new { x.occurence_id, x.event_id });
                    table.CheckConstraint("Day_Cons", "day < 7 AND day > -1");
                    table.ForeignKey(
                        name: "FK_Occurences_Events_event_id",
                        column: x => x.event_id,
                        principalTable: "Events",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Occurences_Rooms_room_code",
                        column: x => x.room_code,
                        principalTable: "Rooms",
                        principalColumn: "room_code");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    section_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    course_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    event_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.section_id);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_course_code",
                        column: x => x.course_code,
                        principalTable: "Courses",
                        principalColumn: "course_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Events_event_id",
                        column: x => x.event_id,
                        principalTable: "Events",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Teaches_",
                columns: table => new
                {
                    instructor_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    section_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teaches_", x => new { x.section_id, x.instructor_id });
                    table.ForeignKey(
                        name: "FK_Teaches__Instructors_instructor_id",
                        column: x => x.instructor_id,
                        principalTable: "Instructors",
                        principalColumn: "instructor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teaches__Sections_section_id",
                        column: x => x.section_id,
                        principalTable: "Sections",
                        principalColumn: "section_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_coordinator_id",
                table: "Courses",
                column: "coordinator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Events_owner",
                table: "Events",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_Instrucor_Ofs_instrucor_id",
                table: "Instrucor_Ofs",
                column: "instrucor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_email_id",
                table: "Instructors",
                column: "email_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occurences_event_id",
                table: "Occurences",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_Occurences_room_code",
                table: "Occurences",
                column: "room_code");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_course_code",
                table: "Sections",
                column: "course_code");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_event_id",
                table: "Sections",
                column: "event_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teaches__instructor_id",
                table: "Teaches_",
                column: "instructor_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instrucor_Ofs");

            migrationBuilder.DropTable(
                name: "Occurences");

            migrationBuilder.DropTable(
                name: "Teaches_");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Instructors");
        }
    }
}
