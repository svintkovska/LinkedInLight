using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class UpdateCertificationAndCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certifications_AspNetUsers_ApplicationUserId",
                table: "Certifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Course",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Certifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "370e206f-5a14-4e0a-b1f9-888c7aac1eb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "abf9a1d5-8c8a-4bac-8f87-f6ae4eea6667");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6d442095-5ad8-44ef-9e23-c09200222963");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "0887e862-80ae-4c26-81c1-eb236f835a8e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "e2bd0d99-81a7-4aba-97bb-d2597f127509");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "0b94787f-a184-45f0-bc25-d02e5bc4c959");

            migrationBuilder.AddForeignKey(
                name: "FK_Certifications_AspNetUsers_ApplicationUserId",
                table: "Certifications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certifications_AspNetUsers_ApplicationUserId",
                table: "Certifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Course",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Certifications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2cea4d9b-9eb7-41d6-bb2d-60ee3c28de92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "11d6e5c6-ecc3-4cf2-ab93-8adf15a6873b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "98265ad6-4adf-4ae7-9554-c857931395b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "5a02c26b-a6d6-4622-a198-18f61b2d0935");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "3e732881-18f7-4a71-abc2-42a0595c2393");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "6502c3c4-11ac-4dd4-b880-abde6b669cfd");

            migrationBuilder.AddForeignKey(
                name: "FK_Certifications_AspNetUsers_ApplicationUserId",
                table: "Certifications",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
