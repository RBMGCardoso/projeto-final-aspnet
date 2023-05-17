using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowSpot.Migrations
{
    /// <inheritdoc />
    public partial class AnoLancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1dbcf925-2666-45dd-8cb9-5617a53712a3");

            migrationBuilder.AddColumn<string>(
                name: "AnoLancamento",
                table: "Conteudos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f6c00383-addf-4516-9b72-d857932ece4b", 0, "b054c780-4e7f-4635-acb8-12564b00c2e9", "rodrigtomas@gmail.com", false, false, null, "RODRIGTOMAS@GMAIL.COM", "RODRIGTOMAS@GMAIL.COM", "AQAAAAIAAYagAAAAEA4073OiGGD7bNkhp+sZxLG3nKGlgkfmdsNoDi1xUFP00hCu1O3S76/OwJNSJs6hzg==", null, false, "b516254e-fa30-4b93-99e1-b2a3a43aca09", false, "rodrigtomas@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6c00383-addf-4516-9b72-d857932ece4b");

            migrationBuilder.DropColumn(
                name: "AnoLancamento",
                table: "Conteudos");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1dbcf925-2666-45dd-8cb9-5617a53712a3", 0, "639e7aec-4f2d-478d-ac2b-cf28ba7000ee", "rodrigtomas@gmail.com", false, false, null, "RODRIGTOMAS@GMAIL.COM", "RODRIGTOMAS@GMAIL.COM", "AQAAAAIAAYagAAAAEB3r+GkBONxl2eGcgK2iFl49vLObxUQOR38IiNG89/7t1u6xpeS1HcP3wqP4StSrcw==", null, false, "aca999ff-908f-4b23-95b9-8a4ec7b22848", false, "rodrigtomas@gmail.com" });
        }
    }
}
