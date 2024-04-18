using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class updatesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38c9a18a-e14c-4c14-b45d-036e42f8eb55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "446b6447-9e1c-4ea6-bcb6-d82c17b5e962");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96671ecb-630b-48d0-a559-588391ab2dd4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd123b58-c76b-4036-b2c2-21bddaa981bb");

            migrationBuilder.AddColumn<string>(
                name: "VoyageIdVoyage",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeVoyage",
                columns: table => new
                {
                    IdTypeVoyage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeVoyage", x => x.IdTypeVoyage);
                });

            migrationBuilder.CreateTable(
                name: "Voyage",
                columns: table => new
                {
                    IdVoyage = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateSortieBon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDepartVoyage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DemandeurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdDemandeur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagasinierId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdMagasinier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeVoyageIdTypeVoyage = table.Column<int>(type: "int", nullable: false),
                    IdTypeVoyage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyage", x => x.IdVoyage);
                    table.ForeignKey(
                        name: "FK_Voyage_AspNetUsers_DemandeurId",
                        column: x => x.DemandeurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Voyage_AspNetUsers_MagasinierId",
                        column: x => x.MagasinierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Voyage_TypeVoyage_TypeVoyageIdTypeVoyage",
                        column: x => x.TypeVoyageIdTypeVoyage,
                        principalTable: "TypeVoyage",
                        principalColumn: "IdTypeVoyage",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtilisateurVoyage",
                columns: table => new
                {
                    UtilisateursID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoyagesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoyageIdVoyage = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateurVoyage", x => new { x.UtilisateursID, x.VoyagesId });
                    table.ForeignKey(
                        name: "FK_UtilisateurVoyage_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UtilisateurVoyage_Voyage_VoyageIdVoyage",
                        column: x => x.VoyageIdVoyage,
                        principalTable: "Voyage",
                        principalColumn: "IdVoyage",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f463cfd-f42c-4f22-afa2-8b4c21ba52ad", null, "Transporteur", "TRANSPORTEUR" },
                    { "be05f52d-221c-493c-9458-88d45fc2fb82", null, "Admin", "ADMIN" },
                    { "cc0bd1f2-3719-4cb9-a96d-d644280632e6", null, "Magasinier", "MAGASINIER" },
                    { "ef46f809-9d19-4aac-98b8-e23de7496cbb", null, "Demandeur", "DEMANDEUR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VoyageIdVoyage",
                table: "AspNetUsers",
                column: "VoyageIdVoyage");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurVoyage_UtilisateurId",
                table: "UtilisateurVoyage",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurVoyage_VoyageIdVoyage",
                table: "UtilisateurVoyage",
                column: "VoyageIdVoyage");

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_DemandeurId",
                table: "Voyage",
                column: "DemandeurId");

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_MagasinierId",
                table: "Voyage",
                column: "MagasinierId");

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_TypeVoyageIdTypeVoyage",
                table: "Voyage",
                column: "TypeVoyageIdTypeVoyage");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Voyage_VoyageIdVoyage",
                table: "AspNetUsers",
                column: "VoyageIdVoyage",
                principalTable: "Voyage",
                principalColumn: "IdVoyage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Voyage_VoyageIdVoyage",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UtilisateurVoyage");

            migrationBuilder.DropTable(
                name: "Voyage");

            migrationBuilder.DropTable(
                name: "TypeVoyage");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VoyageIdVoyage",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f463cfd-f42c-4f22-afa2-8b4c21ba52ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be05f52d-221c-493c-9458-88d45fc2fb82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc0bd1f2-3719-4cb9-a96d-d644280632e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef46f809-9d19-4aac-98b8-e23de7496cbb");

            migrationBuilder.DropColumn(
                name: "VoyageIdVoyage",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38c9a18a-e14c-4c14-b45d-036e42f8eb55", null, "Transporteur", "TRANSPORTEUR" },
                    { "446b6447-9e1c-4ea6-bcb6-d82c17b5e962", null, "Admin", "ADMIN" },
                    { "96671ecb-630b-48d0-a559-588391ab2dd4", null, "Demandeur", "DEMANDEUR" },
                    { "cd123b58-c76b-4036-b2c2-21bddaa981bb", null, "Magasinier", "MAGASINIER" }
                });
        }
    }
}
