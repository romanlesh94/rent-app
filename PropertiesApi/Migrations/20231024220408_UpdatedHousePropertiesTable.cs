using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedHousePropertiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "HousePropertyMappings",
                keyColumn: "Id",
                keyValue: 9L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HousePropertyMappings",
                columns: new[] { "Id", "HouseId", "PropertyId" },
                values: new object[,]
                {
                    { 1L, 1L, 2L },
                    { 2L, 1L, 3L },
                    { 3L, 1L, 5L },
                    { 4L, 2L, 2L },
                    { 5L, 2L, 13L },
                    { 6L, 2L, 11L },
                    { 7L, 3L, 10L },
                    { 8L, 3L, 9L },
                    { 9L, 3L, 7L }
                });
        }
    }
}
