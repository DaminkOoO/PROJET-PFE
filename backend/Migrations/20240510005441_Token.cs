using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Transporteur",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29491d3c-c8ab-478f-b08c-1246b84a8c39", null, "Admin", "ADMIN" },
                    { "7d5f9329-666a-4a46-8335-8df4e83b8ca4", null, "Magasinier", "MAGASINIER" },
                    { "92f53699-6554-4ad5-a3d9-93a0ca287457", null, "Demandeur", "DEMANDEUR" },
                    { "b105a34e-85d2-431e-b009-836a815bdb88", null, "Transporteur", "TRANSPORTEUR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29491d3c-c8ab-478f-b08c-1246b84a8c39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d5f9329-666a-4a46-8335-8df4e83b8ca4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92f53699-6554-4ad5-a3d9-93a0ca287457");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b105a34e-85d2-431e-b009-836a815bdb88");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Transporteur");

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
    }
}
