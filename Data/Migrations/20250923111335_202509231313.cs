using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbhelpuf.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202509231313 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SwishNumber",
                table: "ShopSettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SwishNumber",
                table: "ShopSettings");
        }
    }
}
