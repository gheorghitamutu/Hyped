using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndAPI.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Rights = table.Column<string>(nullable: true),
                    Workplace = table.Column<string>(nullable: true),
                    PositionTitle = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VMs",
                columns: table => new
                {
                    VMId = table.Column<Guid>(nullable: false),
                    RealID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    LastSave = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VMs", x => x.VMId);
                    table.ForeignKey(
                        name: "FK_VMs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VMs_UserId",
                table: "VMs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VMs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
