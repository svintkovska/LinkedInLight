using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class AddPositionAndService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Websites_AspNetUsers_UserId",
                table: "Websites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Websites",
                table: "Websites");

            migrationBuilder.RenameTable(
                name: "Websites",
                newName: "Website");

            migrationBuilder.RenameIndex(
                name: "IX_Websites_UserId",
                table: "Website",
                newName: "IX_Website_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Website",
                table: "Website",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsRemoteOk = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePositions",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePositions", x => new { x.ServiceId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_ServicePositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePositions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "cec37fa0-1acb-4c84-9d8b-05aa93a92092");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ab007ce4-59f8-4573-990a-4bbbfdcba979");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "653e7fe5-f353-4999-81c1-236de93cc1be");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "6913abad-0cde-4d8c-8567-b9586cecfb36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "fcf70c23-422d-47c3-bfe1-222491198abc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "b992c745-0a60-455c-8367-0b827e370849");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePositions_PositionId",
                table: "ServicePositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ApplicationUserId",
                table: "Services",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Website_AspNetUsers_UserId",
                table: "Website",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Website_AspNetUsers_UserId",
                table: "Website");

            migrationBuilder.DropTable(
                name: "ServicePositions");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Website",
                table: "Website");

            migrationBuilder.RenameTable(
                name: "Website",
                newName: "Websites");

            migrationBuilder.RenameIndex(
                name: "IX_Website_UserId",
                table: "Websites",
                newName: "IX_Websites_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Websites",
                table: "Websites",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a7fd92ed-deda-457a-a257-85dd441e3450");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b7d2b7cb-51bf-4559-8290-2e3dde5e4229");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "f16feb3a-237f-425a-9550-626fbef5ed0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "b5a588a6-fc19-47ea-8e08-e593c2f166d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "cd31585c-1f1f-4127-a81c-e2619670b4af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "34762cab-31af-4423-b5a7-6e9543fba742");

            migrationBuilder.AddForeignKey(
                name: "FK_Websites_AspNetUsers_UserId",
                table: "Websites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
