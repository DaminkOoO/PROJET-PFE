using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class relationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transporteur_Localisation_LocalisationId",
                table: "Transporteur");

            migrationBuilder.DropIndex(
                name: "IX_Transporteur_LocalisationId",
                table: "Transporteur");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f4f40c7-1e2e-40a3-9388-0ca9079b28ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31643ef4-9300-4905-8ba5-15cc72754004");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5db227ef-58b4-4b98-ab05-855b42847592");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc09be34-b8da-4e5d-88e5-0698d09c538e");

            migrationBuilder.AlterColumn<int>(
                name: "LocalisationId",
                table: "Transporteur",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e5ea9cb-7080-4c81-9b1b-949980fc67b4", null, "Demandeur", "DEMANDEUR" },
                    { "108fdb99-80c4-41b6-a0b7-94e1c54b80b9", null, "Admin", "ADMIN" },
                    { "4277712e-b7fc-4c4f-b5fb-c39ba3074520", null, "Magasinier", "MAGASINIER" },
                    { "6f0e3d40-90df-42e6-b1c9-3ae17b5a989b", null, "Transporteur", "TRANSPORTEUR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transporteur_LocalisationId",
                table: "Transporteur",
                column: "LocalisationId",
                unique: true,
                filter: "[LocalisationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transporteur_Localisation_LocalisationId",
                table: "Transporteur",
                column: "LocalisationId",
                principalTable: "Localisation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transporteur_Localisation_LocalisationId",
                table: "Transporteur");

            migrationBuilder.DropIndex(
                name: "IX_Transporteur_LocalisationId",
                table: "Transporteur");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e5ea9cb-7080-4c81-9b1b-949980fc67b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "108fdb99-80c4-41b6-a0b7-94e1c54b80b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4277712e-b7fc-4c4f-b5fb-c39ba3074520");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f0e3d40-90df-42e6-b1c9-3ae17b5a989b");

            migrationBuilder.AlterColumn<int>(
                name: "LocalisationId",
                table: "Transporteur",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f4f40c7-1e2e-40a3-9388-0ca9079b28ea", null, "Admin", "ADMIN" },
                    { "31643ef4-9300-4905-8ba5-15cc72754004", null, "Demandeur", "DEMANDEUR" },
                    { "5db227ef-58b4-4b98-ab05-855b42847592", null, "Transporteur", "TRANSPORTEUR" },
                    { "bc09be34-b8da-4e5d-88e5-0698d09c538e", null, "Magasinier", "MAGASINIER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transporteur_LocalisationId",
                table: "Transporteur",
                column: "LocalisationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transporteur_Localisation_LocalisationId",
                table: "Transporteur",
                column: "LocalisationId",
                principalTable: "Localisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
