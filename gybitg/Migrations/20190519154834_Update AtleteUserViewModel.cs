using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gybitg.Migrations
{
    public partial class UpdateAtleteUserViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "APG",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "FGAG",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FGG",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FGMG",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "GP",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GS",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "HighschoolName",
                table: "AthleteUserViewModel",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MPG",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PPG",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RPG",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "AthleteUserViewModel",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "APG",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "FGAG",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "FGG",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "FGMG",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "GP",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "GS",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "HighschoolName",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "MPG",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "PPG",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "RPG",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AthleteUserViewModel");
        }
    }
}
