using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class UpdateVolunteerExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerExperiences_AspNetUsers_ApplicationUserId",
                table: "VolunteerExperiences");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "VolunteerExperiences",
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
                value: "ba33a46d-29bd-4fcf-a404-26969677f3ad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b325c671-b7f7-4746-bd9d-b48740427d84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "930817fe-d061-4028-a959-be951757db7c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "69fd4349-7080-43c9-b6a7-2829bdc9d920");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "0a527b4b-340b-403c-b93e-c86275d8f051");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "27664035-e6e9-453f-a18e-2d83cd964949");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerExperiences_AspNetUsers_ApplicationUserId",
                table: "VolunteerExperiences",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerExperiences_AspNetUsers_ApplicationUserId",
                table: "VolunteerExperiences");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "VolunteerExperiences",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                name: "FK_VolunteerExperiences_AspNetUsers_ApplicationUserId",
                table: "VolunteerExperiences",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
