using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class priorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "Priorite",
                table: "Voyage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Priorite",
                table: "Voyage");

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
        }
    }
}
