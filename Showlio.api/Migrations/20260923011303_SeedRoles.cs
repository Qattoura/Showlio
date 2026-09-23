using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Showlio.api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("43ee448f-61a0-46eb-8c65-463808b39fd2"), "c770e5fc-f65a-45e2-a378-1e743c8f68f1", "Admin", "ADMIN" },
                    { new Guid("9bee4b22-b843-4d88-82cd-8f12958d969d"), "95b12203-fdf1-49c2-9154-ccc25b3c1a50", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("43ee448f-61a0-46eb-8c65-463808b39fd2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9bee4b22-b843-4d88-82cd-8f12958d969d"));
        }
    }
}
