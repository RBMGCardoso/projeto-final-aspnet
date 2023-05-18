using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowSpot.Migrations
{
    /// <inheritdoc />
    public partial class LinkTrailer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkTrailer",
                table: "Conteudos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f46a52c4-9941-40ed-a652-8cd207bf2517");

            migrationBuilder.DropColumn(
                name: "LinkTrailer",
                table: "Conteudos");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f6c00383-addf-4516-9b72-d857932ece4b", 0, "b054c780-4e7f-4635-acb8-12564b00c2e9", "rodrigtomas@gmail.com", false, false, null, "RODRIGTOMAS@GMAIL.COM", "RODRIGTOMAS@GMAIL.COM", "AQAAAAIAAYagAAAAEA4073OiGGD7bNkhp+sZxLG3nKGlgkfmdsNoDi1xUFP00hCu1O3S76/OwJNSJs6hzg==", null, false, "b516254e-fa30-4b93-99e1-b2a3a43aca09", false, "rodrigtomas@gmail.com" });
        }
    }
}
