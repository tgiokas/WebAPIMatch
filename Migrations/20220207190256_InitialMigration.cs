using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPIMatch.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 20, nullable: true),
                    MatchDate = table.Column<DateTime>(nullable: false),
                    MatchTime = table.Column<TimeSpan>(nullable: false),
                    TeamA = table.Column<string>(maxLength: 20, nullable: true),
                    TeamB = table.Column<string>(maxLength: 20, nullable: true),
                    Sport = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchOdds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(nullable: false),
                    Specifier = table.Column<string>(maxLength: 1, nullable: true),
                    Odd = table.Column<decimal>(type: "decimal(4, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOdds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchOdds_Match",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UC_Match",
                table: "Match",
                columns: new[] { "TeamA", "TeamB", "Sport" },
                unique: true,
                filter: "[TeamA] IS NOT NULL AND [TeamB] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UC_MatchOdds",
                table: "MatchOdds",
                columns: new[] { "MatchId", "Specifier" },
                unique: true,
                filter: "[Specifier] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOdds");

            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
