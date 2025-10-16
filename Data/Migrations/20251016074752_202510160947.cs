using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbhelpuf.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202510160947 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "CustomerInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info",
                table: "CustomerInfos");
        }
    }
}
