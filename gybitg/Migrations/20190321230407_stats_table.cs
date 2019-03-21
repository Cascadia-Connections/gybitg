using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace gybitg.Migrations
{
    public partial class stats_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleteStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachProfiles",
                table: "CoachProfiles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoachProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CoachProfiles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachProfiles",
                table: "CoachProfiles",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Blocks = table.Column<int>(type: "int", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Rebounds = table.Column<int>(type: "int", nullable: false),
                    Steals = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Blocks = table.Column<int>(type: "int", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Rebounds = table.Column<int>(type: "int", nullable: false),
                    StatusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Steals = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "StatViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachProfiles",
                table: "CoachProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CoachProfiles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoachProfiles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachProfiles",
                table: "CoachProfiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AthleteStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APG = table.Column<decimal>(nullable: false),
                    DateOFEntry = table.Column<DateTime>(nullable: false),
                    FGAG = table.Column<decimal>(nullable: false),
                    FGG = table.Column<decimal>(nullable: false),
                    FGMG = table.Column<decimal>(nullable: false),
                    FTAG = table.Column<decimal>(nullable: false),
                    FTMG = table.Column<decimal>(nullable: false),
                    FTP = table.Column<decimal>(nullable: false),
                    GP = table.Column<int>(nullable: false),
                    GS = table.Column<int>(nullable: false),
                    MPG = table.Column<decimal>(nullable: false),
                    PPG = table.Column<decimal>(nullable: false),
                    RPG = table.Column<decimal>(nullable: false),
                    TPAG = table.Column<decimal>(nullable: false),
                    TPMG = table.Column<decimal>(nullable: false),
                    TPP = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteStats", x => x.Id);
                });
        }
    }
}
