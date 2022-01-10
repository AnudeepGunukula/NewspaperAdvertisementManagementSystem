using Microsoft.EntityFrameworkCore.Migrations;

namespace NewspaperAdvertisementManagementSystem.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_ClientId1",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ClientId1",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Advertisements");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Advertisements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ClientId",
                table: "Advertisements",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_ClientId",
                table: "Advertisements",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_ClientId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ClientId",
                table: "Advertisements");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Advertisements",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId1",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ClientId1",
                table: "Advertisements",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_ClientId1",
                table: "Advertisements",
                column: "ClientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
