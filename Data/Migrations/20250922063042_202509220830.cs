using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbhelpuf.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202509220830 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LogoImageId",
                table: "ShopSettings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShopSettings_LogoImageId",
                table: "ShopSettings",
                column: "LogoImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopSettings_Images_LogoImageId",
                table: "ShopSettings",
                column: "LogoImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopSettings_Images_LogoImageId",
                table: "ShopSettings");

            migrationBuilder.DropIndex(
                name: "IX_ShopSettings_LogoImageId",
                table: "ShopSettings");

            migrationBuilder.DropColumn(
                name: "LogoImageId",
                table: "ShopSettings");
        }
    }
}
