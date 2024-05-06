using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class mapUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41faa13b-7bd9-487e-bf94-3035688f85af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bc6d62a-27c6-48b4-900e-91b583b5704e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cbf4a6b-8742-476e-9dd2-f302e8904ba3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb6cd39a-49a6-4962-aff7-6e8db253445f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f4f40c7-1e2e-40a3-9388-0ca9079b28ea", null, "Admin", "ADMIN" },
                    { "31643ef4-9300-4905-8ba5-15cc72754004", null, "Demandeur", "DEMANDEUR" },
                    { "5db227ef-58b4-4b98-ab05-855b42847592", null, "Transporteur", "TRANSPORTEUR" },
                    { "bc09be34-b8da-4e5d-88e5-0698d09c538e", null, "Magasinier", "MAGASINIER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f4f40c7-1e2e-40a3-9388-0ca9079b28ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31643ef4-9300-4905-8ba5-15cc72754004");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5db227ef-58b4-4b98-ab05-855b42847592");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc09be34-b8da-4e5d-88e5-0698d09c538e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41faa13b-7bd9-487e-bf94-3035688f85af", null, "Transporteur", "TRANSPORTEUR" },
                    { "4bc6d62a-27c6-48b4-900e-91b583b5704e", null, "Magasinier", "MAGASINIER" },
                    { "8cbf4a6b-8742-476e-9dd2-f302e8904ba3", null, "Admin", "ADMIN" },
                    { "cb6cd39a-49a6-4962-aff7-6e8db253445f", null, "Demandeur", "DEMANDEUR" }
                });
        }
    }
}
