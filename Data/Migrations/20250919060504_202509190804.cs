using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbhelpuf.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202509190804 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopContactInfo_ShopSocialMedia_SocialMediasId",
                table: "ShopContactInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopSettings_ShopContactInfo_ContactInfoId",
                table: "ShopSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopSocialMedia",
                table: "ShopSocialMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopContactInfo",
                table: "ShopContactInfo");

            migrationBuilder.RenameTable(
                name: "ShopSocialMedia",
                newName: "ShopSocialMedias");

            migrationBuilder.RenameTable(
                name: "ShopContactInfo",
                newName: "ShopContactInfos");

            migrationBuilder.RenameIndex(
                name: "IX_ShopContactInfo_SocialMediasId",
                table: "ShopContactInfos",
                newName: "IX_ShopContactInfos_SocialMediasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopSocialMedias",
                table: "ShopSocialMedias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopContactInfos",
                table: "ShopContactInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopContactInfos_ShopSocialMedias_SocialMediasId",
                table: "ShopContactInfos",
                column: "SocialMediasId",
                principalTable: "ShopSocialMedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSettings_ShopContactInfos_ContactInfoId",
                table: "ShopSettings",
                column: "ContactInfoId",
                principalTable: "ShopContactInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopContactInfos_ShopSocialMedias_SocialMediasId",
                table: "ShopContactInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopSettings_ShopContactInfos_ContactInfoId",
                table: "ShopSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopSocialMedias",
                table: "ShopSocialMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopContactInfos",
                table: "ShopContactInfos");

            migrationBuilder.RenameTable(
                name: "ShopSocialMedias",
                newName: "ShopSocialMedia");

            migrationBuilder.RenameTable(
                name: "ShopContactInfos",
                newName: "ShopContactInfo");

            migrationBuilder.RenameIndex(
                name: "IX_ShopContactInfos_SocialMediasId",
                table: "ShopContactInfo",
                newName: "IX_ShopContactInfo_SocialMediasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopSocialMedia",
                table: "ShopSocialMedia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopContactInfo",
                table: "ShopContactInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopContactInfo_ShopSocialMedia_SocialMediasId",
                table: "ShopContactInfo",
                column: "SocialMediasId",
                principalTable: "ShopSocialMedia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSettings_ShopContactInfo_ContactInfoId",
                table: "ShopSettings",
                column: "ContactInfoId",
                principalTable: "ShopContactInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
