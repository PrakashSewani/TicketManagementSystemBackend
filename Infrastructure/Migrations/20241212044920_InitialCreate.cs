using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheatreTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TheatreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheatreTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTable_RoleTable_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheatreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieTable_TheatreTable_TheatreId",
                        column: x => x.TheatreId,
                        principalTable: "TheatreTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScreenTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TheatreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenTable_MovieTable_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScreenTable_TheatreTable_TheatreId",
                        column: x => x.TheatreId,
                        principalTable: "TheatreTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SeatTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatTable_ScreenTable_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "ScreenTable",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RoleTable",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("3cab6048-0f28-4d60-828a-882cc2baa90d"), "TheatreOwner" },
                    { new Guid("459a516b-7e76-4aca-a759-1ae1b100aa2d"), "PlatformAdmin" },
                    { new Guid("6f2a960a-7d3b-4f61-881b-22ee6c319948"), "TheatreUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieTable_TheatreId",
                table: "MovieTable",
                column: "TheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTable_MovieId",
                table: "ScreenTable",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTable_TheatreId",
                table: "ScreenTable",
                column: "TheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatTable_ScreenId",
                table: "SeatTable",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_RoleId",
                table: "UserTable",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatTable");

            migrationBuilder.DropTable(
                name: "UserTable");

            migrationBuilder.DropTable(
                name: "ScreenTable");

            migrationBuilder.DropTable(
                name: "RoleTable");

            migrationBuilder.DropTable(
                name: "MovieTable");

            migrationBuilder.DropTable(
                name: "TheatreTable");
        }
    }
}
