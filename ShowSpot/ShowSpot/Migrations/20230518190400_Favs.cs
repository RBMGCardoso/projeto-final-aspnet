using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowSpot.Migrations
{
    /// <inheritdoc />
    public partial class Favs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false),
                    ConteudosFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoritos_Conteudos_ConteudosFK",
                        column: x => x.ConteudosFK,
                        principalTable: "Conteudos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoritos_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_ConteudosFK",
                table: "Favoritos",
                column: "ConteudosFK");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UtilizadorFK",
                table: "Favoritos",
                column: "UtilizadorFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98d4d891-065f-4fcb-bbe5-ee8c620fa4d0");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "af50bcff-1f0c-41c5-a700-6ec48d96e266", 0, "1c9d4cb2-a989-446e-a33d-79ebd610eb10", "rodrigtomas@gmail.com", false, false, null, "RODRIGTOMAS@GMAIL.COM", "RODRIGTOMAS@GMAIL.COM", "AQAAAAIAAYagAAAAEMzIuJm64NIGZO8/Kf/cRSBDTZdAowgN2IZiJoQR+G3YvjTp5G+3X2BvyCHmfwPTRw==", null, false, "672afc21-cb2a-485e-85c7-e01a9123a08a", false, "rodrigtomas@gmail.com" });
        }
    }
}
