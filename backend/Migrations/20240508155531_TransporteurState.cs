using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TransporteurState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Transporteur",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Transporteur");

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
    }
}
