using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertiesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HouseProperties",
                columns: new[] { "Id", "PropertyText" },
                values: new object[,]
                {
                    { 1L, "Free Wi-Fi" },
                    { 2L, "King-size bed" },
                    { 3L, "Free parking" },
                    { 4L, "Breakfast included" },
                    { 5L, "Pet-friendly" },
                    { 6L, "Non-smoking" },
                    { 7L, "Swimming pool" },
                    { 8L, "Safe for children" },
                    { 9L, "Transfer from the airport" },
                    { 10L, "Beautiful view" },
                    { 11L, "BBQ" },
                    { 12L, "Balcony" },
                    { 13L, "Outside area" },
                    { 14L, "TV" },
                    { 15L, "Washing machine" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "HouseProperties",
                keyColumn: "Id",
                keyValue: 15L);
        }
    }
}
