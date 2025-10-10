using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbhelpuf.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202510080939 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Addressee",
                table: "CustomerInfos",
                newName: "StreetName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CustomerInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CustomerInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "CustomerInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CustomerInfos");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CustomerInfos");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "CustomerInfos");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "CustomerInfos",
                newName: "Addressee");
        }
    }
}
