using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlatformAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleTable",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { new Guid("459a516b-7e76-4aca-a759-1ae1b100aa2d"), "PlatformAdmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleTable",
                keyColumn: "Id",
                keyValue: new Guid("459a516b-7e76-4aca-a759-1ae1b100aa2d"));
        }
    }
}
