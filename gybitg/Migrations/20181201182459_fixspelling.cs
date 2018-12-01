using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace gybitg.Migrations
{
    public partial class fixspelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileViedoUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ProfileVideoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileVideoUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ProfileViedoUrl",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
