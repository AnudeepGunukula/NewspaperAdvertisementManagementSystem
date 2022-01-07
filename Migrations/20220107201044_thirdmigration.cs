using Microsoft.EntityFrameworkCore.Migrations;

namespace NewspaperAdvertisementManagementSystem.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvertisementImageName",
                table: "Advertisements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdvertisementImageName",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
