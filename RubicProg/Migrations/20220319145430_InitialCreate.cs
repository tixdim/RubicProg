using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RubicProg.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserWhoProfileId = table.Column<int>(nullable: false),
                    IsBoy = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DateRegistration = table.Column<DateTimeOffset>(nullable: false),
                    Birthday = table.Column<DateTimeOffset>(nullable: false),
                    AvatarUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileUsers_Users_UserWhoProfileId",
                        column: x => x.UserWhoProfileId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserWhoTrainingId = table.Column<int>(nullable: false),
                    WorkoutTime = table.Column<int>(nullable: false),
                    Exercise = table.Column<string>(nullable: true),
                    IsDone = table.Column<bool>(nullable: false),
                    StartWorkoutDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_Users_UserWhoTrainingId",
                        column: x => x.UserWhoTrainingId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsers_UserWhoProfileId",
                table: "ProfileUsers",
                column: "UserWhoProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserWhoTrainingId",
                table: "Workouts",
                column: "UserWhoTrainingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileUsers");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
