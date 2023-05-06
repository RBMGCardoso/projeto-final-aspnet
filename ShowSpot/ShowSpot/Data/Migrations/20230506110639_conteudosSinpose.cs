using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowSpot.Data.Migrations
{
    /// <inheritdoc />
    public partial class conteudosSinpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Conteudos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Conteudos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
