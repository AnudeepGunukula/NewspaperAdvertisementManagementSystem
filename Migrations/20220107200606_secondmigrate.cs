using Microsoft.EntityFrameworkCore.Migrations;

namespace NewspaperAdvertisementManagementSystem.Migrations
{
    public partial class secondmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdvertisementImageName",
                table: "Advertisements",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdvertisementImageName",
                table: "Advertisements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
