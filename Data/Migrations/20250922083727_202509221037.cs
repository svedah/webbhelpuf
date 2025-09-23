using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbhelpuf.Data.Migrations
{
    /// <inheritdoc />
    public partial class _202509221037 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ShopItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ShopItems");
        }
    }
}
