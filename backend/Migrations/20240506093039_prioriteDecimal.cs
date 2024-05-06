using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class prioriteDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1db80f95-78f1-46e2-8643-26fc964762de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29ae24b6-33e2-455d-8fcd-a0b38f716127");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30dc0590-6d21-45bb-9080-8a54d6fc5992");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1d85476-9fd6-4ea3-b1df-237475f8dbc8");

            migrationBuilder.AlterColumn<decimal>(
                name: "Priorite",
                table: "Voyage",
                type: "decimal(20,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Priorite",
                table: "Voyage",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,17)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1db80f95-78f1-46e2-8643-26fc964762de", null, "Transporteur", "TRANSPORTEUR" },
                    { "29ae24b6-33e2-455d-8fcd-a0b38f716127", null, "Demandeur", "DEMANDEUR" },
                    { "30dc0590-6d21-45bb-9080-8a54d6fc5992", null, "Magasinier", "MAGASINIER" },
                    { "d1d85476-9fd6-4ea3-b1df-237475f8dbc8", null, "Admin", "ADMIN" }
                });
        }
    }
}
