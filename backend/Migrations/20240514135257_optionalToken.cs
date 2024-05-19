using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class optionalToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "178c85cb-f913-4ee9-bbb8-89e4130ada15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37d65eca-d9a4-4cc0-832a-b1472826d72d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ee119d6-525b-433a-af78-e34c051b9fb5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebe49d1c-b8ed-4e61-a028-92a540709cb3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49534573-fbf9-4351-bea5-887ed3551854", null, "Magasinier", "MAGASINIER" },
                    { "5304ce0f-79ab-49f6-8445-c1c65adc3977", null, "Transporteur", "TRANSPORTEUR" },
                    { "592c4fc7-af4a-4502-b51e-9e77ae6efd9d", null, "Admin", "ADMIN" },
                    { "fc520e41-e765-4205-9312-4dd7787d1ebc", null, "Demandeur", "DEMANDEUR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49534573-fbf9-4351-bea5-887ed3551854");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5304ce0f-79ab-49f6-8445-c1c65adc3977");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "592c4fc7-af4a-4502-b51e-9e77ae6efd9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc520e41-e765-4205-9312-4dd7787d1ebc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "178c85cb-f913-4ee9-bbb8-89e4130ada15", null, "Magasinier", "MAGASINIER" },
                    { "37d65eca-d9a4-4cc0-832a-b1472826d72d", null, "Demandeur", "DEMANDEUR" },
                    { "8ee119d6-525b-433a-af78-e34c051b9fb5", null, "Transporteur", "TRANSPORTEUR" },
                    { "ebe49d1c-b8ed-4e61-a028-92a540709cb3", null, "Admin", "ADMIN" }
                });
        }
    }
}
