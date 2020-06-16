using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZenTime.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "zt");

            migrationBuilder.CreateTable(
                name: "ProjectDefinitions",
                schema: "zt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskDefinitions",
                schema: "zt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSheetProjectDefinitionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    DetailsRequired = table.Column<bool>(nullable: false),
                    WeeklyTargetHours = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskDefinitions_ProjectDefinitions_TimeSheetProjectDefinitionId",
                        column: x => x.TimeSheetProjectDefinitionId,
                        principalSchema: "zt",
                        principalTable: "ProjectDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetEntries",
                schema: "zt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSheetTaskDefinitionId = table.Column<int>(nullable: false),
                    Details = table.Column<string>(maxLength: 500, nullable: true),
                    DurationInMins = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSheetEntries_TaskDefinitions_TimeSheetTaskDefinitionId",
                        column: x => x.TimeSheetTaskDefinitionId,
                        principalSchema: "zt",
                        principalTable: "TaskDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDefinitions_TimeSheetProjectDefinitionId",
                schema: "zt",
                table: "TaskDefinitions",
                column: "TimeSheetProjectDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetEntries_TimeSheetTaskDefinitionId",
                schema: "zt",
                table: "TimeSheetEntries",
                column: "TimeSheetTaskDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSheetEntries",
                schema: "zt");

            migrationBuilder.DropTable(
                name: "TaskDefinitions",
                schema: "zt");

            migrationBuilder.DropTable(
                name: "ProjectDefinitions",
                schema: "zt");
        }
    }
}
