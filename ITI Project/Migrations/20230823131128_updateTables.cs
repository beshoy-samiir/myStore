using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Project.Migrations
{
    /// <inheritdoc />
    public partial class updateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPrice",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
