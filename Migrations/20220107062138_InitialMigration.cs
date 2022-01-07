using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NewspaperAdvertisementManagementSystem.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    SubscriberId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubscriberDesc = table.Column<string>(nullable: true),
                    CompanyEmail = table.Column<string>(nullable: true),
                    Popularity = table.Column<string>(nullable: true),
                    OfficeAddress = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.SubscriberId);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    AdvertisementId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertisementType = table.Column<string>(nullable: true),
                    AdvertisementTitle = table.Column<string>(nullable: true),
                    AdvertisementDesc = table.Column<string>(nullable: true),
                    SubscriptionPlan = table.Column<string>(nullable: true),
                    AdvertisementSize = table.Column<string>(nullable: true),
                    Subscriber = table.Column<string>(nullable: true),
                    AdvRegisteredDate = table.Column<DateTime>(nullable: false),
                    AdvPublishedDate = table.Column<DateTime>(nullable: false),
                    Agree = table.Column<bool>(nullable: false),
                    AdminApproved = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.AdvertisementId);
                    table.ForeignKey(
                        name: "FK_Advertisements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentAmount = table.Column<int>(nullable: false),
                    PaymentType = table.Column<string>(nullable: true),
                    UpiId = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<bool>(nullable: false),
                    PaymentTime = table.Column<DateTime>(nullable: false),
                    AdvertisementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "AdvertisementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ClientId",
                table: "Advertisements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AdvertisementId",
                table: "Payments",
                column: "AdvertisementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
