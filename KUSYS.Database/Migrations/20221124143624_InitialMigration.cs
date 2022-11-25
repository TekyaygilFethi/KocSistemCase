using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS.Database.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName" },
                values: new object[,]
                {
                    { "CSI101", "Introduction to Computer Science" },
                    { "CSI102", "Algorithms" },
                    { "MAT101", "Calculus" },
                    { "PHY101", "Physics" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ModifiedDate", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("66ab82f0-9892-4740-af46-97ffbd028d0c"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6727), "0IFYCPfTeVTHCiS7fJmv+QgQFlk=", 0, "yaseminozen" },
                    { new Guid("777b651d-c644-45f7-af95-2c2299d51db3"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6741), "VOL7WalKDWYMXYkBgtCPx1Etx44=", 0, "tahatekyaygil" },
                    { new Guid("f9960263-5cef-4ade-ada1-ba4768fbac37"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6718), "fe230b6xtLrMo+Ab+HWdNI6hzsE=", 1, "fethitekyaygil" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "Lastname", "ModifiedDate", "UserId" },
                values: new object[] { new Guid("0a64fd1e-0cc9-4995-88ee-f60cf056ffe1"), new DateTime(1996, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fethi", "Tekyaygil", new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6742), new Guid("f9960263-5cef-4ade-ada1-ba4768fbac37") });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "Lastname", "ModifiedDate", "UserId" },
                values: new object[] { new Guid("2c2425ec-158d-439c-937d-907bb804c509"), new DateTime(2008, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taha", "Tekyaygil", new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6752), new Guid("777b651d-c644-45f7-af95-2c2299d51db3") });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "Lastname", "ModifiedDate", "UserId" },
                values: new object[] { new Guid("6d9214af-cd43-40b8-829f-a9261daa82b5"), new DateTime(1997, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yasemin", "Özen", new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6749), new Guid("66ab82f0-9892-4740-af46-97ffbd028d0c") });

            migrationBuilder.InsertData(
                table: "StudentCourses",
                columns: new[] { "CourseId", "StudentId", "ModifiedDate" },
                values: new object[,]
                {
                    { "CSI101", new Guid("0a64fd1e-0cc9-4995-88ee-f60cf056ffe1"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6747) },
                    { "MAT101", new Guid("0a64fd1e-0cc9-4995-88ee-f60cf056ffe1"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6748) },
                    { "CSI101", new Guid("2c2425ec-158d-439c-937d-907bb804c509"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6753) },
                    { "CSI102", new Guid("2c2425ec-158d-439c-937d-907bb804c509"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6758) },
                    { "MAT101", new Guid("2c2425ec-158d-439c-937d-907bb804c509"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6759) },
                    { "CSI102", new Guid("6d9214af-cd43-40b8-829f-a9261daa82b5"), new DateTime(2022, 11, 24, 17, 36, 24, 773, DateTimeKind.Local).AddTicks(6751) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
