using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class magasinierDemandeur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "375191bb-3d26-4c5c-b495-c71eadbafa14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6448cf13-d265-4120-b645-3f9137e3c0e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73fd1754-a4ce-49cb-8a71-a152eda1ffe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d028fa0-61bd-45ba-9d24-0e6f1a7c0a3a");

            migrationBuilder.AddColumn<string>(
                name: "MagasinierDemandeurMatriculeSopal",
                table: "Voyage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "284874d0-4463-436b-b11a-51edb478cde7", null, "Magasinier", "MAGASINIER" },
                    { "2cac978c-f9b4-4d8d-86a6-b812a37303be", null, "Transporteur", "TRANSPORTEUR" },
                    { "37cb1cb6-e1e8-45c5-ad3d-7415a9c274f2", null, "Demandeur", "DEMANDEUR" },
                    { "a58d179d-41b2-4d71-b670-445f29a7aa8f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_MagasinierDemandeurMatriculeSopal",
                table: "Voyage",
                column: "MagasinierDemandeurMatriculeSopal");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Magasinier_MagasinierDemandeurMatriculeSopal",
                table: "Voyage",
                column: "MagasinierDemandeurMatriculeSopal",
                principalTable: "Magasinier",
                principalColumn: "MatriculeSopal",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Magasinier_MagasinierDemandeurMatriculeSopal",
                table: "Voyage");

            migrationBuilder.DropIndex(
                name: "IX_Voyage_MagasinierDemandeurMatriculeSopal",
                table: "Voyage");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "284874d0-4463-436b-b11a-51edb478cde7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2cac978c-f9b4-4d8d-86a6-b812a37303be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37cb1cb6-e1e8-45c5-ad3d-7415a9c274f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a58d179d-41b2-4d71-b670-445f29a7aa8f");

            migrationBuilder.DropColumn(
                name: "MagasinierDemandeurMatriculeSopal",
                table: "Voyage");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "375191bb-3d26-4c5c-b495-c71eadbafa14", null, "Transporteur", "TRANSPORTEUR" },
                    { "6448cf13-d265-4120-b645-3f9137e3c0e5", null, "Admin", "ADMIN" },
                    { "73fd1754-a4ce-49cb-8a71-a152eda1ffe8", null, "Magasinier", "MAGASINIER" },
                    { "7d028fa0-61bd-45ba-9d24-0e6f1a7c0a3a", null, "Demandeur", "DEMANDEUR" }
                });
        }
    }
}
