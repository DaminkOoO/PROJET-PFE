using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ISLIBRETrue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c3ead24-e59b-470b-9e00-3d9666dcddc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aab4db9b-2145-4ba6-bceb-d8ad3829ab67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f511bd08-ed01-4c6c-8e92-891a02570910");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f82c7fe1-62a2-4eba-a847-be9d59e8f96f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1520bb5e-395c-48bd-9e67-6d3514ba091a", null, "Demandeur", "DEMANDEUR" },
                    { "3bdb57a0-0336-4acd-9dc2-fec966180c69", null, "Magasinier", "MAGASINIER" },
                    { "7cde3150-9f20-4c10-b334-fc60b2e392a7", null, "Admin", "ADMIN" },
                    { "b11df156-bcb0-4f39-a9a1-a96b6c99708d", null, "Transporteur", "TRANSPORTEUR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1520bb5e-395c-48bd-9e67-6d3514ba091a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bdb57a0-0336-4acd-9dc2-fec966180c69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cde3150-9f20-4c10-b334-fc60b2e392a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b11df156-bcb0-4f39-a9a1-a96b6c99708d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c3ead24-e59b-470b-9e00-3d9666dcddc6", null, "Transporteur", "TRANSPORTEUR" },
                    { "aab4db9b-2145-4ba6-bceb-d8ad3829ab67", null, "Demandeur", "DEMANDEUR" },
                    { "f511bd08-ed01-4c6c-8e92-891a02570910", null, "Magasinier", "MAGASINIER" },
                    { "f82c7fe1-62a2-4eba-a847-be9d59e8f96f", null, "Admin", "ADMIN" }
                });
        }
    }
}
