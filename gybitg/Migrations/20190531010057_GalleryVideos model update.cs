using Microsoft.EntityFrameworkCore.Migrations;

namespace gybitg.Migrations
{
    public partial class GalleryVideosmodelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo1",
                table: "AthleteUserViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo2",
                table: "AthleteUserViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo3",
                table: "AthleteUserViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo4",
                table: "AthleteUserViewModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo2",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo3",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryVideo4",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GalleryVideo1",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "GalleryVideo2",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "GalleryVideo3",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "GalleryVideo4",
                table: "AthleteUserViewModel");

            migrationBuilder.DropColumn(
                name: "GalleryVideo1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GalleryVideo2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GalleryVideo3",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GalleryVideo4",
                table: "AspNetUsers");
        }
    }
}
