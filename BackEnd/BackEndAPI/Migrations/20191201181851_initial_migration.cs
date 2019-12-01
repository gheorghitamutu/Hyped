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
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    Country = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 35, nullable: false),
                    Rights = table.Column<string>(maxLength: 30, nullable: false)
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
                    RealID = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Configuration = table.Column<string>(maxLength: 50, nullable: false),
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Country", "FirstName", "LastName", "Password", "Rights", "Username" },
                values: new object[,]
                {
                    { new Guid("514b68ed-00ca-404a-8568-6bded2026954"), "Romania", "AAA", "BBB", "blablabla192", "rwx", "ababa124" },
                    { new Guid("7cc2eb8c-141a-4be3-b2dd-a51633f0fcc2"), "Germany", "CCCC", "DD", "cd113", "rx", "cd1234" }
                });

            migrationBuilder.InsertData(
                table: "VMs",
                columns: new[] { "VMId", "Configuration", "LastSave", "Name", "RealID", "UserId" },
                values: new object[,]
                {
                    { new Guid("a52f37c8-8c28-4ab8-99bc-6c72c08fbe43"), "vms/sag1_s15sag3g9121410/config.json", "/vms/sag1_s15sag3g9121410/save.json", "MyFirstVM1", "sag1_s15sag3g9121410", null },
                    { new Guid("9403657a-7cf2-4abe-a3b2-3e89dbe55e25"), "vms/sag1_s15sag3g9121asa1210/config.json", "/vms/sag1_s15sag3g9121asa1210/save.json", "MyFirstVM2", "sag1_s15sag3g9121asa1210", null },
                    { new Guid("5a729aa7-14f7-436a-bfda-6ea5367d3d74"), "vms/sag1_s15sag3g9121asa1210abcd123/config.json", "/vms/sag1_s15sag3g9121asa1210abcd123/save.json", "MyFirstVM3", "sag1_s15sag3g9121asa1210abcd123", null }
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
