using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BestSellers",
                table: "AppProducts",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Trending",
                table: "AppProducts",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestSellers",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "Trending",
                table: "AppProducts");
        }
    }
}
