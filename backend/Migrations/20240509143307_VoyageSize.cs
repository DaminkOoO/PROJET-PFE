using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class VoyageSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abb93fa-508f-403a-a740-2de3ac770e18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cd6abea-a75a-4a13-bf35-ee52cb7a5f53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d20c846-e6ce-4639-9ec4-3ed9b3de097d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3104244-9a04-4d4c-a1b4-1d1ce590163f");

            migrationBuilder.AlterColumn<decimal>(
                name: "Priorite",
                table: "Voyage",
                type: "decimal(30,25)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,17)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Priorite",
                table: "Voyage",
                type: "decimal(20,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,25)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1abb93fa-508f-403a-a740-2de3ac770e18", null, "Demandeur", "DEMANDEUR" },
                    { "3cd6abea-a75a-4a13-bf35-ee52cb7a5f53", null, "Admin", "ADMIN" },
                    { "3d20c846-e6ce-4639-9ec4-3ed9b3de097d", null, "Magasinier", "MAGASINIER" },
                    { "a3104244-9a04-4d4c-a1b4-1d1ce590163f", null, "Transporteur", "TRANSPORTEUR" }
                });
        }
    }
}
