using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("a6cc23a9-3417-41af-993c-b815f8192246"), "d381c2ea-2462-43a3-be15-4712adf1e5bf", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a6cc23a9-3417-41af-993c-b815f8192246"), new Guid("bfcd74cb-af46-4f94-842d-f8bd1731d8ba") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("bfcd74cb-af46-4f94-842d-f8bd1731d8ba"), 0, "7219af20-3d8d-4a3e-b5cb-9c19a72b73b1", new DateTime(2022, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "fomust1969@gmail.com", true, "Anh", "Tuan", false, null, "fomust1969@gmail.com", "admin", "AQAAAAEAACcQAAAAEA8WgfYjnskZFaR+ePjOKNWCRnx3r2F0IWEf6FbQxrmqhoBB/ZXqNpy7wduUyxkYMA==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 2, 5, 28, 38, 900, DateTimeKind.Local).AddTicks(7786));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6cc23a9-3417-41af-993c-b815f8192246"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a6cc23a9-3417-41af-993c-b815f8192246"), new Guid("bfcd74cb-af46-4f94-842d-f8bd1731d8ba") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bfcd74cb-af46-4f94-842d-f8bd1731d8ba"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 2, 5, 15, 31, 651, DateTimeKind.Local).AddTicks(5764));
        }
    }
}
