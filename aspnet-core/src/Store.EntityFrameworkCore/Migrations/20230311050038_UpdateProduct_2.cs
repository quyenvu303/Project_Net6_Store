using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryParentId",
                table: "AppProducts",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryParentId",
                table: "AppProducts");
        }
    }
}
