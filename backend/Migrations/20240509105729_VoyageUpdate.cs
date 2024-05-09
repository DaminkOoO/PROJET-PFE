using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class VoyageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Magasinier_MagasinierDemandeurMatriculeSopal",
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

            migrationBuilder.AlterColumn<string>(
                name: "MagasinierDemandeurMatriculeSopal",
                table: "Voyage",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DemandeurMatriculeSopal",
                table: "Voyage",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "NbrePalette",
                table: "Voyage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Poids",
                table: "Voyage",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Magasinier_MagasinierDemandeurMatriculeSopal",
                table: "Voyage",
                column: "MagasinierDemandeurMatriculeSopal",
                principalTable: "Magasinier",
                principalColumn: "MatriculeSopal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Magasinier_MagasinierDemandeurMatriculeSopal",
                table: "Voyage");

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

            migrationBuilder.DropColumn(
                name: "NbrePalette",
                table: "Voyage");

            migrationBuilder.DropColumn(
                name: "Poids",
                table: "Voyage");

            migrationBuilder.AlterColumn<string>(
                name: "MagasinierDemandeurMatriculeSopal",
                table: "Voyage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DemandeurMatriculeSopal",
                table: "Voyage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Magasinier_MagasinierDemandeurMatriculeSopal",
                table: "Voyage",
                column: "MagasinierDemandeurMatriculeSopal",
                principalTable: "Magasinier",
                principalColumn: "MatriculeSopal",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
