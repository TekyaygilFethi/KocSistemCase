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
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    { new Guid("154ef833-3f9d-47f5-9dff-2be71ba7590c"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9957), "0IFYCPfTeVTHCiS7fJmv+QgQFlk=", 0, "yaseminozen" },
                    { new Guid("76e7ad2a-a350-47f1-a74d-55b308f16055"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9946), "fe230b6xtLrMo+Ab+HWdNI6hzsE=", 1, "fethitekyaygil" },
                    { new Guid("de0d7f96-b99c-49bd-9750-9dbedf8619da"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9965), "VOL7WalKDWYMXYkBgtCPx1Etx44=", 0, "tahatekyaygil" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "Lastname", "ModifiedDate", "UserId" },
                values: new object[] { new Guid("9993ac14-534a-4d37-8798-c9fb009f755f"), new DateTime(1997, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yasemin", "Özen", new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9976), new Guid("154ef833-3f9d-47f5-9dff-2be71ba7590c") });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "Lastname", "ModifiedDate", "UserId" },
                values: new object[] { new Guid("a42ef0fb-4aad-47df-85ab-29837004da30"), new DateTime(2008, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taha", "Tekyaygil", new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9979), new Guid("de0d7f96-b99c-49bd-9750-9dbedf8619da") });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "Lastname", "ModifiedDate", "UserId" },
                values: new object[] { new Guid("f6ca5ed4-a6ac-4b8c-9304-2b4de4369cf2"), new DateTime(1996, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fethi", "Tekyaygil", new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9967), new Guid("76e7ad2a-a350-47f1-a74d-55b308f16055") });

            migrationBuilder.InsertData(
                table: "StudentCourses",
                columns: new[] { "CourseId", "StudentId", "ModifiedDate" },
                values: new object[,]
                {
                    { "CSI102", new Guid("9993ac14-534a-4d37-8798-c9fb009f755f"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9978) },
                    { "CSI101", new Guid("a42ef0fb-4aad-47df-85ab-29837004da30"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9980) },
                    { "CSI102", new Guid("a42ef0fb-4aad-47df-85ab-29837004da30"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9980) },
                    { "MAT101", new Guid("a42ef0fb-4aad-47df-85ab-29837004da30"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9981) },
                    { "CSI101", new Guid("f6ca5ed4-a6ac-4b8c-9304-2b4de4369cf2"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9974) },
                    { "MAT101", new Guid("f6ca5ed4-a6ac-4b8c-9304-2b4de4369cf2"), new DateTime(2022, 11, 16, 15, 57, 24, 658, DateTimeKind.Local).AddTicks(9975) }
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
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
