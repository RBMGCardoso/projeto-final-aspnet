using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowSpot.Data.Migrations
{
    /// <inheritdoc />
    public partial class Show : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conteudos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    ImgUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteudos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "ConteudoTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConteudoFK = table.Column<int>(type: "INTEGER", nullable: false),
                    TagFK = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConteudoTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConteudoTags_Conteudos_ConteudoFK",
                        column: x => x.ConteudoFK,
                        principalTable: "Conteudos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConteudoTags_Tags_TagFK",
                        column: x => x.TagFK,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recomendados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtilizadorFK = table.Column<int>(type: "INTEGER", nullable: false),
                    ConteudosFK = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recomendados_Conteudos_ConteudosFK",
                        column: x => x.ConteudosFK,
                        principalTable: "Conteudos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recomendados_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchLaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtilizadorFK = table.Column<int>(type: "INTEGER", nullable: false),
                    ConteudosFK = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLaters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchLaters_Conteudos_ConteudosFK",
                        column: x => x.ConteudosFK,
                        principalTable: "Conteudos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchLaters_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConteudoTags_ConteudoFK",
                table: "ConteudoTags",
                column: "ConteudoFK");

            migrationBuilder.CreateIndex(
                name: "IX_ConteudoTags_TagFK",
                table: "ConteudoTags",
                column: "TagFK");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendados_ConteudosFK",
                table: "Recomendados",
                column: "ConteudosFK");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendados_UtilizadorFK",
                table: "Recomendados",
                column: "UtilizadorFK");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLaters_ConteudosFK",
                table: "WatchLaters",
                column: "ConteudosFK");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLaters_UtilizadorFK",
                table: "WatchLaters",
                column: "UtilizadorFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConteudoTags");

            migrationBuilder.DropTable(
                name: "Recomendados");

            migrationBuilder.DropTable(
                name: "WatchLaters");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Conteudos");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
