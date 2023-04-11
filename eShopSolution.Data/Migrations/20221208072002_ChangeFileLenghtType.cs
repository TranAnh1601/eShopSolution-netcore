using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileLenghtType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6cc23a9-3417-41af-993c-b815f8192246"),
                column: "ConcurrencyStamp",
                value: "16bba320-f666-42cd-be61-4c1ac84c226a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bfcd74cb-af46-4f94-842d-f8bd1731d8ba"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0955e7a8-fbba-4def-93f8-2035276701d5", "AQAAAAEAACcQAAAAEJ8Q644GKefRDUbFURH32H4wLF0TlKTNSAip3WHp2oi1WN/tbP+rd1Q0vWbZZTNeXQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 8, 14, 20, 2, 248, DateTimeKind.Local).AddTicks(700));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6cc23a9-3417-41af-993c-b815f8192246"),
                column: "ConcurrencyStamp",
                value: "408ef7b2-881e-4fa6-9837-0a34dd830860");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bfcd74cb-af46-4f94-842d-f8bd1731d8ba"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98a9bf7e-73c8-4bf7-a3ab-f2f5b1a8e26b", "AQAAAAEAACcQAAAAEL4beahHfUQLSQslj4rCP3pYWKoCjs+9FBZmP09ZLqAVinWcae7m7hF4WhvhRb/2kw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 5, 18, 56, 14, 375, DateTimeKind.Local).AddTicks(2722));
        }
    }
}
