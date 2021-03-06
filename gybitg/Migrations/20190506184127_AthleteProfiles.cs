﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace gybitg.Migrations
{
    public partial class AthleteProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AAUCoach",
                table: "AthleteProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AAUCoach",
                table: "AthleteProfiles");
        }
    }
}
