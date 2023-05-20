using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowSpot.Migrations
{
    /// <inheritdoc />
    public partial class AspnetUserReplacement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UtilizadorFK",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "UtilizadorFK",
                table: "Favoritos");

            migrationBuilder.AlterColumn<string>(
                name: "UtilizadorFK",
                table: "WatchLaters",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserFK",
                table: "Favoritos",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad6bb408-9ae1-4dbc-adfc-34f1155d4b93", "AQAAAAIAAYagAAAAEARWuCOyv6nTWB2y+ZBtu4x66XAl/1lyLoikdanl/ygkzUypUv+jIwGyCFS3hh+lYw==", "f83d035d-e74f-40c8-a953-5aacc98439f6" });

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UserFK",
                table: "Favoritos",
                column: "UserFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_AspNetUsers_UserFK",
                table: "Favoritos",
                column: "UserFK",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_AspNetUsers_UtilizadorFK",
                table: "WatchLaters",
                column: "UtilizadorFK",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_AspNetUsers_UserFK",
                table: "Favoritos");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaters_AspNetUsers_UtilizadorFK",
                table: "WatchLaters");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UserFK",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "UserFK",
                table: "Favoritos");

            migrationBuilder.AlterColumn<int>(
                name: "UtilizadorFK",
                table: "WatchLaters",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UtilizadorFK",
                table: "Favoritos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f32a6eb-46c2-4476-b77e-c1169f2868b0", "AQAAAAIAAYagAAAAEMFTqwjcOFZHO115q7knlAE3shplHK9wfLni9QIV1Nfojk/I37faZIkdpQs9sgePPw==", "fa3a82be-2de3-4495-92b7-874565a7c6e0" });

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UtilizadorFK",
                table: "Favoritos",
                column: "UtilizadorFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_Utilizadores_UtilizadorFK",
                table: "Favoritos",
                column: "UtilizadorFK",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_Utilizadores_UtilizadorFK",
                table: "WatchLaters",
                column: "UtilizadorFK",
                principalTable: "Utilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
