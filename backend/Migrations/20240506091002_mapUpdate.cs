using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class mapUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b59b72f-4d77-42f8-8300-64f9f740df22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e69ed89-7690-4a3e-8fe2-2c04eeb7d710");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af0d3c84-0ee3-45ed-9643-cb9ca7cae752");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec99a865-6064-4370-a772-221096a9f2fa");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "2b59b72f-4d77-42f8-8300-64f9f740df22", null, "Admin", "ADMIN" },
                    { "4e69ed89-7690-4a3e-8fe2-2c04eeb7d710", null, "Transporteur", "TRANSPORTEUR" },
                    { "af0d3c84-0ee3-45ed-9643-cb9ca7cae752", null, "Magasinier", "MAGASINIER" },
                    { "ec99a865-6064-4370-a772-221096a9f2fa", null, "Demandeur", "DEMANDEUR" }
                });
        }
    }
}
