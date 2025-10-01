using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CreateScheduleAndInstallments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationInMonths = table.Column<int>(nullable: false),
                    AnnualRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnlockDate = table.Column<DateTime>(nullable: false),
                    MonthlyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => new { x.ScheduleId, x.Number });
                    table.ForeignKey(
                        name: "FK_Installments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Installments");
            migrationBuilder.DropTable(name: "Schedules");
        }
    }
}
