using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NewspaperAdvertisementManagementSystem.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Advertisements_AdvertisementId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AdvertisementId",
                table: "Payments");

            migrationBuilder.AddColumn<long>(
                name: "AdvertisementId1",
                table: "Payments",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AdvertisementId",
                table: "Advertisements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AdvertisementId1",
                table: "Payments",
                column: "AdvertisementId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Advertisements_AdvertisementId1",
                table: "Payments",
                column: "AdvertisementId1",
                principalTable: "Advertisements",
                principalColumn: "AdvertisementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Advertisements_AdvertisementId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AdvertisementId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AdvertisementId1",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "Advertisements",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AdvertisementId",
                table: "Payments",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Advertisements_AdvertisementId",
                table: "Payments",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "AdvertisementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
