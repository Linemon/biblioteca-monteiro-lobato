using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminEmployeeExample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Name", "Email", "Phone", "PasswordHash", "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    "00000000-0000-0000-0000-000000000000",
                    "Admin User",
                    "admin@example.com",
                    "11999999999",
                    "admin123",
                    DateTime.UtcNow,
                    DateTime.UtcNow
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000"
            );
        }
    }
}
