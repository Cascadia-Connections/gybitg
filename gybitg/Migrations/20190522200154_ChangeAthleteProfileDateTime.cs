using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gybitg.Migrations
{
    public partial class ChangeAthleteProfileDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HSGraduationDate",
                table: "AthleteProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "DateOfBirth",
                table: "AthleteProfiles",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HSGraduationDate",
                table: "AthleteProfiles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AthleteProfiles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
