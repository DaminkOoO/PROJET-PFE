using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class poidVolumeActuel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36364ad5-a1c0-4cee-b628-74409af3de77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6873b8b4-792e-4540-b274-8c2db82edfac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23f7b22-5cb7-482a-ba08-4de89bd4c8fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be0d6657-ba29-48a1-8605-00b660754c46");

            migrationBuilder.AddColumn<double>(
                name: "PoidsActuel",
                table: "Vehicule",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VolumeActuel",
                table: "Vehicule",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PoidsActuel",
                table: "Vehicule");

            migrationBuilder.DropColumn(
                name: "VolumeActuel",
                table: "Vehicule");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36364ad5-a1c0-4cee-b628-74409af3de77", null, "Demandeur", "DEMANDEUR" },
                    { "6873b8b4-792e-4540-b274-8c2db82edfac", null, "Admin", "ADMIN" },
                    { "a23f7b22-5cb7-482a-ba08-4de89bd4c8fc", null, "Transporteur", "TRANSPORTEUR" },
                    { "be0d6657-ba29-48a1-8605-00b660754c46", null, "Magasinier", "MAGASINIER" }
                });
        }
    }
}
