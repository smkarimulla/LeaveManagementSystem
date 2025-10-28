using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e587f44c-3215-4d4f-a21c-5f6a72151d0b", "3b380f13-8a61-4b26-9658-05dac0aff78a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b380f13-8a61-4b26-9658-05dac0aff78a",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8703ae5-d85c-49a2-a057-fa7267ab4e04", new DateOnly(1990, 1, 1), "Default", "Admin", "AQAAAAIAAYagAAAAEPqNqgwIK8EP6jeKIu+RDaPpwpqBOJr3FVbpvcy86avyp2Uxr/S0uZB0Sigecbb8zw==", "2eec564b-67f0-4c88-8eb0-9fa5c5a23a2c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e587f44c-3215-4d4f-a21c-5f6a72151d0b", "3b380f13-8a61-4b26-9658-05dac0aff78a" });

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b380f13-8a61-4b26-9658-05dac0aff78a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1a02391-4375-4880-af94-f4cb2a2b5149", "AQAAAAIAAYagAAAAEC5ru7+49oFUofFByYQX7sjQiWdSGpCve+OLqmATbfft/kYL4adf92HRsyGJip7UOw==", "dd13f353-1ae7-448c-9113-b0ac24db27b8" });
        }
    }
}
