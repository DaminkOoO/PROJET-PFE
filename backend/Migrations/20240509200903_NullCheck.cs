using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class NullCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4314166e-b731-4323-a194-4d48557bd97b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bf31c0e-58dd-44af-8443-40cdd71b0c97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83aadd4d-314d-4191-a850-5a789ecf0f77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3d87913-8163-48cf-a540-14642104d990");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDepartVoyage",
                table: "Voyage",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "df13f693-1460-4c1a-94c3-54caf01393a0", null, "Admin", "ADMIN" },
                    { "e7e72bf7-301d-4ffb-a96f-37125729bfa1", null, "Magasinier", "MAGASINIER" },
                    { "f2e7971b-18eb-4e86-92fa-fbe1d4a18003", null, "Transporteur", "TRANSPORTEUR" },
                    { "fcdd6e01-7b47-4278-9a91-1fdc061d3f23", null, "Demandeur", "DEMANDEUR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df13f693-1460-4c1a-94c3-54caf01393a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7e72bf7-301d-4ffb-a96f-37125729bfa1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2e7971b-18eb-4e86-92fa-fbe1d4a18003");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcdd6e01-7b47-4278-9a91-1fdc061d3f23");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDepartVoyage",
                table: "Voyage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4314166e-b731-4323-a194-4d48557bd97b", null, "Admin", "ADMIN" },
                    { "5bf31c0e-58dd-44af-8443-40cdd71b0c97", null, "Demandeur", "DEMANDEUR" },
                    { "83aadd4d-314d-4191-a850-5a789ecf0f77", null, "Magasinier", "MAGASINIER" },
                    { "d3d87913-8163-48cf-a540-14642104d990", null, "Transporteur", "TRANSPORTEUR" }
                });
        }
    }
}
