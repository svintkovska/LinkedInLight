using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class AddProfileVisitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OpenToHire",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OpenToWork",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfileVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VisitDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileVisits_AspNetUsers_ProfileOwnerId",
                        column: x => x.ProfileOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfileVisits_AspNetUsers_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "90f469a2-f4a6-4e33-93bd-fc0395ed3b6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "622aa333-3c42-4d94-955a-025b7f40d5a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e3c2c4ff-4742-4e4b-9cf7-4f39c766c445");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "663da768-af30-4d92-a9ec-687a43ba6749");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "699a39b5-9a1e-44fb-8a74-1061f4c756ee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "55a49bbc-b5ad-4af8-9b26-11ff9b9fd0d6");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileVisits_ProfileOwnerId",
                table: "ProfileVisits",
                column: "ProfileOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileVisits_VisitorId",
                table: "ProfileVisits",
                column: "VisitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileVisits");

            migrationBuilder.DropColumn(
                name: "OpenToHire",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OpenToWork",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "29beeebf-c7ac-452f-8448-2d697549eb6d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "733f8f65-2f7b-4a1d-a1e5-d8006d288919");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "d0979352-373d-469f-93b4-4667eafcba1e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "4cd3ca34-c09b-4000-bbed-86942fe1569a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "82aa5933-82c3-4516-aa17-8f183061297b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "01853bc9-bcad-4ac2-81be-6c3385d46ea5");
        }
    }
}
