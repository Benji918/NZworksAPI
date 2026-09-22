using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZworks.Migrations.NzWalksAuthDB
{
    /// <inheritdoc />
    public partial class Creatingauthdatabas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b06d5c15-71f9-4b9e-8458-ebfe760c3209");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd16187e-d88e-4802-8d1e-689b2656882f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56947673-b045-441e-8229-45b14d81c694", "f446eb96-a2f8-4133-825a-4d23442ebbf0", "Admin", "ADMIN" },
                    { "f446eb96-a2f8-4133-825a-4d23442ebbf0", "f446eb96-a2f8-4133-825a-4d23442ebbf0", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56947673-b045-441e-8229-45b14d81c694");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f446eb96-a2f8-4133-825a-4d23442ebbf0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b06d5c15-71f9-4b9e-8458-ebfe760c3209", "1fba102a-7920-4a67-bac3-2feb3a01a856", "Admin", "ADMIN" },
                    { "fd16187e-d88e-4802-8d1e-689b2656882f", "d1c43336-9cd5-43bc-b245-27ef477765f2", "User", "USER" }
                });
        }
    }
}
