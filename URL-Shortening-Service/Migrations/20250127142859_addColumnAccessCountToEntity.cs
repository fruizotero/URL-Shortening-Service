using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URL_Shortening_Service.Migrations
{
    /// <inheritdoc />
    public partial class addColumnAccessCountToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessCount",
                table: "ShortUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessCount",
                table: "ShortUrls");
        }
    }
}
