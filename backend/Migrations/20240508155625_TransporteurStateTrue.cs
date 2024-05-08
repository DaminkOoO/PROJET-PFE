using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TransporteurStateTrue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ba215e4-d1a1-4dad-a112-c53336c76dbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc9ce0d0-b67a-47c1-8f65-70a515ad427a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3680957-7be0-45dc-ac50-ea32bc8c70c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4670eb3-8c34-401b-bef8-6101d80cb142");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "375191bb-3d26-4c5c-b495-c71eadbafa14", null, "Transporteur", "TRANSPORTEUR" },
                    { "6448cf13-d265-4120-b645-3f9137e3c0e5", null, "Admin", "ADMIN" },
                    { "73fd1754-a4ce-49cb-8a71-a152eda1ffe8", null, "Magasinier", "MAGASINIER" },
                    { "7d028fa0-61bd-45ba-9d24-0e6f1a7c0a3a", null, "Demandeur", "DEMANDEUR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "375191bb-3d26-4c5c-b495-c71eadbafa14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6448cf13-d265-4120-b645-3f9137e3c0e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73fd1754-a4ce-49cb-8a71-a152eda1ffe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d028fa0-61bd-45ba-9d24-0e6f1a7c0a3a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ba215e4-d1a1-4dad-a112-c53336c76dbb", null, "Magasinier", "MAGASINIER" },
                    { "cc9ce0d0-b67a-47c1-8f65-70a515ad427a", null, "Admin", "ADMIN" },
                    { "d3680957-7be0-45dc-ac50-ea32bc8c70c0", null, "Transporteur", "TRANSPORTEUR" },
                    { "d4670eb3-8c34-401b-bef8-6101d80cb142", null, "Demandeur", "DEMANDEUR" }
                });
        }
    }
}
