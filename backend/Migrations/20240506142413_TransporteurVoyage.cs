using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TransporteurVoyage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a4ced6e-1247-4ddb-a580-24d9cdf501c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22b12c2e-7062-479b-8568-fcb69738a1a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e78c579-ccbf-4097-832c-775525d17014");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afcb2d30-93f6-4ad5-b08a-ca1e8e60261e");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "1a4ced6e-1247-4ddb-a580-24d9cdf501c9", null, "Transporteur", "TRANSPORTEUR" },
                    { "22b12c2e-7062-479b-8568-fcb69738a1a5", null, "Magasinier", "MAGASINIER" },
                    { "7e78c579-ccbf-4097-832c-775525d17014", null, "Admin", "ADMIN" },
                    { "afcb2d30-93f6-4ad5-b08a-ca1e8e60261e", null, "Demandeur", "DEMANDEUR" }
                });
        }
    }
}
