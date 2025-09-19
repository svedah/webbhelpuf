using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbhelpuf.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202509181100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HostName",
                table: "ShopSettings",
                newName: "ContactInfoId");

            migrationBuilder.CreateTable(
                name: "ShopSocialMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Facebook = table.Column<string>(type: "TEXT", nullable: false),
                    Instagram = table.Column<string>(type: "TEXT", nullable: false),
                    LinkedIn = table.Column<string>(type: "TEXT", nullable: false),
                    TikTok = table.Column<string>(type: "TEXT", nullable: false),
                    YouTube = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopSocialMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopContactInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    MobileNumber = table.Column<string>(type: "TEXT", nullable: false),
                    SocialMediasId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopContactInfo_ShopSocialMedia_SocialMediasId",
                        column: x => x.SocialMediasId,
                        principalTable: "ShopSocialMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopSettings_ContactInfoId",
                table: "ShopSettings",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopContactInfo_SocialMediasId",
                table: "ShopContactInfo",
                column: "SocialMediasId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSettings_ShopContactInfo_ContactInfoId",
                table: "ShopSettings",
                column: "ContactInfoId",
                principalTable: "ShopContactInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopSettings_ShopContactInfo_ContactInfoId",
                table: "ShopSettings");

            migrationBuilder.DropTable(
                name: "ShopContactInfo");

            migrationBuilder.DropTable(
                name: "ShopSocialMedia");

            migrationBuilder.DropIndex(
                name: "IX_ShopSettings_ContactInfoId",
                table: "ShopSettings");

            migrationBuilder.RenameColumn(
                name: "ContactInfoId",
                table: "ShopSettings",
                newName: "HostName");
        }
    }
}
