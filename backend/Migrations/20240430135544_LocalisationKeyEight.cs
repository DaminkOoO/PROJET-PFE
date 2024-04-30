using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class LocalisationKeyEight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    MatriculeSopal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entrepot",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lattitude = table.Column<decimal>(type: "decimal(20,17)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(20,17)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrepot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localisation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lattitude = table.Column<decimal>(type: "decimal(20,17)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(20,17)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localisation", x => x.Id);
                });

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
                name: "Vehicule",
                columns: table => new
                {
                    Matricule = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poids = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Bloque = table.Column<bool>(type: "bit", nullable: false),
                    IsLibre = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicule", x => x.Matricule);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Demandeur",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lattitude = table.Column<decimal>(type: "decimal(20,17)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(20,17)", nullable: false),
                    IsMagasinier = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandeur", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Demandeur_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Magasinier",
                columns: table => new
                {
                    MatriculeSopal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntrepotId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magasinier", x => x.MatriculeSopal);
                    table.ForeignKey(
                        name: "FK_Magasinier_AspNetUsers_MatriculeSopal",
                        column: x => x.MatriculeSopal,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Magasinier_Entrepot_EntrepotId",
                        column: x => x.EntrepotId,
                        principalTable: "Entrepot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transporteur",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehiculeMatricule = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExterneDuService = table.Column<bool>(type: "bit", nullable: false),
                    LocalisationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporteur", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Transporteur_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transporteur_Localisation_LocalisationId",
                        column: x => x.LocalisationId,
                        principalTable: "Localisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transporteur_Vehicule_VehiculeMatricule",
                        column: x => x.VehiculeMatricule,
                        principalTable: "Vehicule",
                        principalColumn: "Matricule");
                });

            migrationBuilder.CreateTable(
                name: "Voyage",
                columns: table => new
                {
                    IdVoyage = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateSortieBon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDepartVoyage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DemandeurMatriculeSopal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MagasinierMatriculeSopal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeVoyageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyage", x => x.IdVoyage);
                    table.ForeignKey(
                        name: "FK_Voyage_Demandeur_DemandeurMatriculeSopal",
                        column: x => x.DemandeurMatriculeSopal,
                        principalTable: "Demandeur",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Voyage_Magasinier_MagasinierMatriculeSopal",
                        column: x => x.MagasinierMatriculeSopal,
                        principalTable: "Magasinier",
                        principalColumn: "MatriculeSopal");
                    table.ForeignKey(
                        name: "FK_Voyage_TypeVoyage_TypeVoyageId",
                        column: x => x.TypeVoyageId,
                        principalTable: "TypeVoyage",
                        principalColumn: "IdTypeVoyage",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransporteurVoyage",
                columns: table => new
                {
                    TransporteurMatriculeSopal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoyageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Panne = table.Column<bool>(type: "bit", nullable: false),
                    VoyageIdVoyage = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TransporteurUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LocalisationsId = table.Column<int>(type: "int", nullable: false)
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
                    { "2b59b72f-4d77-42f8-8300-64f9f740df22", null, "Admin", "ADMIN" },
                    { "4e69ed89-7690-4a3e-8fe2-2c04eeb7d710", null, "Transporteur", "TRANSPORTEUR" },
                    { "af0d3c84-0ee3-45ed-9643-cb9ca7cae752", null, "Magasinier", "MAGASINIER" },
                    { "ec99a865-6064-4370-a772-221096a9f2fa", null, "Demandeur", "DEMANDEUR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Magasinier_EntrepotId",
                table: "Magasinier",
                column: "EntrepotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transporteur_LocalisationId",
                table: "Transporteur",
                column: "LocalisationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transporteur_VehiculeMatricule",
                table: "Transporteur",
                column: "VehiculeMatricule");

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

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_DemandeurMatriculeSopal",
                table: "Voyage",
                column: "DemandeurMatriculeSopal");

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_MagasinierMatriculeSopal",
                table: "Voyage",
                column: "MagasinierMatriculeSopal");

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_TypeVoyageId",
                table: "Voyage",
                column: "TypeVoyageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TransporteurVoyage");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Transporteur");

            migrationBuilder.DropTable(
                name: "Voyage");

            migrationBuilder.DropTable(
                name: "Localisation");

            migrationBuilder.DropTable(
                name: "Vehicule");

            migrationBuilder.DropTable(
                name: "Demandeur");

            migrationBuilder.DropTable(
                name: "Magasinier");

            migrationBuilder.DropTable(
                name: "TypeVoyage");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Entrepot");
        }
    }
}
