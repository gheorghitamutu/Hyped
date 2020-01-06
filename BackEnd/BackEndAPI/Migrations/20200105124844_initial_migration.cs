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
                    RAM = table.Column<int>(nullable: false),
                    Cores = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VMs", x => x.VMId);
                    table.ForeignKey(
                        name: "FK_VMs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    NetId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    InstanceID = table.Column<string>(nullable: true),
                    VMId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.NetId);
                    table.ForeignKey(
                        name: "FK_Networks_VMs_VMId",
                        column: x => x.VMId,
                        principalTable: "VMs",
                        principalColumn: "VMId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SCs",
                columns: table => new
                {
                    SCId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    InstanceId = table.Column<string>(nullable: true),
                    VMId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCs", x => x.SCId);
                    table.ForeignKey(
                        name: "FK_SCs_VMs_VMId",
                        column: x => x.VMId,
                        principalTable: "VMs",
                        principalColumn: "VMId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CDVDs",
                columns: table => new
                {
                    CDDVDId = table.Column<Guid>(nullable: false),
                    InstanceId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SCId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDVDs", x => x.CDDVDId);
                    table.ForeignKey(
                        name: "FK_CDVDs_SCs_SCId",
                        column: x => x.SCId,
                        principalTable: "SCs",
                        principalColumn: "SCId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VHDs",
                columns: table => new
                {
                    VHDId = table.Column<Guid>(nullable: false),
                    SCId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    InstanceId = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VHDs", x => x.VHDId);
                    table.ForeignKey(
                        name: "FK_VHDs_SCs_SCId",
                        column: x => x.SCId,
                        principalTable: "SCs",
                        principalColumn: "SCId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CDVDs_SCId",
                table: "CDVDs",
                column: "SCId");

            migrationBuilder.CreateIndex(
                name: "IX_Networks_VMId",
                table: "Networks",
                column: "VMId");

            migrationBuilder.CreateIndex(
                name: "IX_SCs_VMId",
                table: "SCs",
                column: "VMId");

            migrationBuilder.CreateIndex(
                name: "IX_VHDs_SCId",
                table: "VHDs",
                column: "SCId");

            migrationBuilder.CreateIndex(
                name: "IX_VMs_UserId",
                table: "VMs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CDVDs");

            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropTable(
                name: "VHDs");

            migrationBuilder.DropTable(
                name: "SCs");

            migrationBuilder.DropTable(
                name: "VMs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
