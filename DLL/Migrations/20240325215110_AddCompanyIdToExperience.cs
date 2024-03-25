using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class AddCompanyIdToExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Experiences");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Experiences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "abe7db0e-26c6-4e5a-8ddc-9cfb2218766e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "26cfe145-287d-44a4-9aa9-a3c20f8ed935");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "2236ddcd-3d5f-41c8-b338-791006c8c242");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "d92e9736-cfae-4e03-8324-5a1f4920460b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "936895ae-8294-465f-a364-76b400f41983");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "96ebd194-ec5a-48db-b58c-00c572bd0892");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CompanyId",
                table: "Experiences",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Companies_CompanyId",
                table: "Experiences",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Companies_CompanyId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_CompanyId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Experiences");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3ffdd25e-ea1e-4bd3-aeda-097cd2bf8706");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cf205330-a975-4de7-9767-19272c00213e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "f2f0fb0d-29d9-4c1e-b436-45823d19bd17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "b73ab10c-ea88-42a4-a339-9779509e5cb6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "2017135b-4ca0-4abf-b105-cce8ce9d1826");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "294f9975-93b4-4863-a52f-3e830752ee22");
        }
    }
}
