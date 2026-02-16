using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnetAssignment.Migrations
{
    /// <inheritdoc />
    public partial class PhotoPathColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Tourists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Tourists",
                columns: new[] { "Id", "Address", "Name", "PhotoPath", "Rating", "Type" },
                values: new object[,]
                {
                    { 1L, "Cox's Bazar", "Cox bazar Sea Beach", null, 3.5, 0 },
                    { 2L, "Khulna", "SundarBan", null, 4.0, 3 },
                    { 3L, "Lalbag", "Lalbag Fort", null, 3.0, 3 },
                    { 4L, "Cumilla", "Mohasthan Gor", null, 4.2000000000000002, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tourists",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Tourists",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Tourists",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tourists",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Tourists");
        }
    }
}
