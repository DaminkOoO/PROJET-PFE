using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TransporteurOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransporteurVoyage");

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

            migrationBuilder.AddColumn<string>(
                name: "TransporteurId",
                table: "Voyage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_TransporteurId",
                table: "Voyage",
                column: "TransporteurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voyage_Transporteur_TransporteurId",
                table: "Voyage",
                column: "TransporteurId",
                principalTable: "Transporteur",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voyage_Transporteur_TransporteurId",
                table: "Voyage");

            migrationBuilder.DropIndex(
                name: "IX_Voyage_TransporteurId",
                table: "Voyage");

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

            migrationBuilder.DropColumn(
                name: "TransporteurId",
                table: "Voyage");

            migrationBuilder.CreateTable(
                name: "TransporteurVoyage",
                columns: table => new
                {
                    TransporteurMatriculeSopal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoyageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalisationsId = table.Column<int>(type: "int", nullable: false),
                    TransporteurUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoyageIdVoyage = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Panne = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransporteurVoyage", x => new { x.TransporteurMatriculeSopal, x.VoyageId });
                    table.ForeignKey(
                        name: "FK_TransporteurVoyage_Localisation_LocalisationsId",
                        column: x => x.LocalisationsId,
                        principalTable: "Localisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransporteurVoyage_Transporteur_TransporteurMatriculeSopal",
                        column: x => x.TransporteurMatriculeSopal,
                        principalTable: "Transporteur",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_TransporteurVoyage_Transporteur_TransporteurUserId",
                        column: x => x.TransporteurUserId,
                        principalTable: "Transporteur",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_TransporteurVoyage_Voyage_VoyageId",
                        column: x => x.VoyageId,
                        principalTable: "Voyage",
                        principalColumn: "IdVoyage");
                    table.ForeignKey(
                        name: "FK_TransporteurVoyage_Voyage_VoyageIdVoyage",
                        column: x => x.VoyageIdVoyage,
                        principalTable: "Voyage",
                        principalColumn: "IdVoyage");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TransporteurVoyage_LocalisationsId",
                table: "TransporteurVoyage",
                column: "LocalisationsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransporteurVoyage_TransporteurUserId",
                table: "TransporteurVoyage",
                column: "TransporteurUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransporteurVoyage_VoyageId",
                table: "TransporteurVoyage",
                column: "VoyageId");

            migrationBuilder.CreateIndex(
                name: "IX_TransporteurVoyage_VoyageIdVoyage",
                table: "TransporteurVoyage",
                column: "VoyageIdVoyage");
        }
    }
}
