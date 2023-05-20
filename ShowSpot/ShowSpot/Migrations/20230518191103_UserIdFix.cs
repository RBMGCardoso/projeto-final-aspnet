using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowSpot.Migrations
{
    /// <inheritdoc />
    public partial class UserIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "994985ce-3913-4372-8f85-c8a08c760e00", "rodrigtomas@gmail.com", false, false, null, "RODRIGTOMAS@GMAIL.COM", "RODRIGTOMAS@GMAIL.COM", "AQAAAAIAAYagAAAAEAe/5zoxYlAlyAhDaMRsfkAucTrpjoQhjjL1rntAAyYeKYIg+xcO86jpIvz9+zSuQQ==", null, false, "a11d47d7-d303-4ae7-8e1e-d8fcffba8e62", false, "rodrigtomas@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "98d4d891-065f-4fcb-bbe5-ee8c620fa4d0", 0, "c48e04a9-d887-4474-9a78-a2d3fd2ffe40", "rodrigtomas@gmail.com", false, false, null, "RODRIGTOMAS@GMAIL.COM", "RODRIGTOMAS@GMAIL.COM", "AQAAAAIAAYagAAAAEMizBUajL/xjugvsw+QCSrpqdW7XIeXuuU2i2PHGYnT+ikTezr+kV6TyqWELy6avmA==", null, false, "ec3c70b0-c234-44dd-ada8-ae06127dd212", false, "rodrigtomas@gmail.com" });
        }
    }
}
