using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72fd8756-84aa-40c6-aece-86163790ec34", null, "Supervisor", "SUPERVISOR" },
                    { "bf987263-05fc-41e9-9cd8-e81527d9dcf3", null, "Employee", "EMPLOYEE" },
                    { "e587f44c-3215-4d4f-a21c-5f6a72151d0b", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3b380f13-8a61-4b26-9658-05dac0aff78a", 0, "f1a02391-4375-4880-af94-f4cb2a2b5149", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEC5ru7+49oFUofFByYQX7sjQiWdSGpCve+OLqmATbfft/kYL4adf92HRsyGJip7UOw==", null, false, "dd13f353-1ae7-448c-9113-b0ac24db27b8", false, "admin@localhost.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72fd8756-84aa-40c6-aece-86163790ec34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf987263-05fc-41e9-9cd8-e81527d9dcf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e587f44c-3215-4d4f-a21c-5f6a72151d0b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b380f13-8a61-4b26-9658-05dac0aff78a");
        }
    }
}
