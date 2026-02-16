using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnetAssignment.Migrations
{
    /// <inheritdoc />
    public partial class Initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tourists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourists", x => x.Id);
                });

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
            migrationBuilder.DropTable(
                name: "Tourists");
        }
    }
}
