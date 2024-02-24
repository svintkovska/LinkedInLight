using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class UpdateExistingModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Namme",
                table: "Skills",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Experiences",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Educations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b99d559c-f302-45c1-9423-c03f06c98edf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "933d1c11-0c00-4eba-8d3b-e1a5d0227cc8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "5f873361-d7bc-43a1-8471-f850c9aec42d");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ApplicationUserId",
                table: "Skills",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_ApplicationUserId",
                table: "Skills",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_ApplicationUserId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ApplicationUserId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Skills",
                newName: "Namme");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Experiences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Educations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e836682b-d413-432a-a833-191a101aec74");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a0ef73cb-586f-4cab-96b7-25c215d7d4f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "8f644a2d-705f-4340-b27d-b57331565728");
        }
    }
}
