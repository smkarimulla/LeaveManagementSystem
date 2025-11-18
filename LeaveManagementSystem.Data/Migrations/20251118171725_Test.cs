using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b380f13-8a61-4b26-9658-05dac0aff78a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd789408-2e31-4773-8732-12f0474159cb", "AQAAAAIAAYagAAAAEEfhaNBuxKjl5Xlh9B8Kc3gd3QrqIJ2XAgPhlBC8DvzciUm8Q00HZPxlC9/u9VmeLw==", "e31ee650-0fee-495b-9db8-3169d2036372" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b380f13-8a61-4b26-9658-05dac0aff78a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0262150b-240e-4c84-97c6-359c6d1259f2", "AQAAAAIAAYagAAAAEO3PKzjrOv7Ft2KJKfpdMgNYIfXKVp/iCfkIlYZWlWV/sMj/412Q0Nkx0EK0aKpxRg==", "6c324274-2415-439d-a861-5d9636017536" });
        }
    }
}
