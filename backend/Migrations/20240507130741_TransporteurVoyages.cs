using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TransporteurVoyages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59b5d026-5e33-48e4-84d9-ff8c333e1a76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75b0a4a0-5e0d-4b1b-9877-28ea161abb43");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76baaffb-96f2-49e5-bf73-d4f869ec8fde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8dd2932-f308-4343-bdb4-1d06e62d1bdc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03016a1a-50e4-4e25-8367-a627e2922f11", null, "Transporteur", "TRANSPORTEUR" },
                    { "85955afc-cd92-4242-a6c6-df4644088dfa", null, "Demandeur", "DEMANDEUR" },
                    { "94c3798a-a0b7-48e7-9c95-b8acb0e0e22c", null, "Magasinier", "MAGASINIER" },
                    { "d50213b4-8e0e-4a5d-b6cf-3f3c9241ab24", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03016a1a-50e4-4e25-8367-a627e2922f11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85955afc-cd92-4242-a6c6-df4644088dfa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94c3798a-a0b7-48e7-9c95-b8acb0e0e22c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d50213b4-8e0e-4a5d-b6cf-3f3c9241ab24");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59b5d026-5e33-48e4-84d9-ff8c333e1a76", null, "Magasinier", "MAGASINIER" },
                    { "75b0a4a0-5e0d-4b1b-9877-28ea161abb43", null, "Admin", "ADMIN" },
                    { "76baaffb-96f2-49e5-bf73-d4f869ec8fde", null, "Transporteur", "TRANSPORTEUR" },
                    { "b8dd2932-f308-4343-bdb4-1d06e62d1bdc", null, "Demandeur", "DEMANDEUR" }
                });
        }
    }
}
