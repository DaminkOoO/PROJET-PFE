using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TokenMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e7c79a3-167d-403f-8103-f81ad30e0e13");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1347a5bf-d3f3-4a42-842b-8f369021318d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "710f36c2-4a59-498e-b2ef-86f242c0838e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba5eae5f-0d2d-49ad-b9c8-b22e7f74ba6d");

            migrationBuilder.AddColumn<string>(
                name: "TokenMessage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TokenMessage",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e7c79a3-167d-403f-8103-f81ad30e0e13", null, "Magasinier", "MAGASINIER" },
                    { "1347a5bf-d3f3-4a42-842b-8f369021318d", null, "Transporteur", "TRANSPORTEUR" },
                    { "710f36c2-4a59-498e-b2ef-86f242c0838e", null, "Demandeur", "DEMANDEUR" },
                    { "ba5eae5f-0d2d-49ad-b9c8-b22e7f74ba6d", null, "Admin", "ADMIN" }
                });
        }
    }
}
