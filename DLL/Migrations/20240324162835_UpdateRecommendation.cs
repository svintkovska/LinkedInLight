using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class UpdateRecommendation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_AspNetUsers_GivenByUserId",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_AspNetUsers_ReceivedByUserId",
                table: "Recommendations");

            migrationBuilder.RenameColumn(
                name: "ReceivedByUserId",
                table: "Recommendations",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "GivenByUserId",
                table: "Recommendations",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "GivenAt",
                table: "Recommendations",
                newName: "DateRequested");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_ReceivedByUserId",
                table: "Recommendations",
                newName: "IX_Recommendations_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_GivenByUserId",
                table: "Recommendations",
                newName: "IX_Recommendations_ReceiverId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateGiven",
                table: "Recommendations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RequestMessage",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d2b84db4-f4de-40ab-aaed-fd0da3c50863");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "86212030-0300-4d8b-8a47-c42446daf71a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "ca8f10c6-8280-48bc-81ac-135a5b5c8eb5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "e1869d9f-f117-48b0-a1b7-e283bee1b29b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "07665412-fd86-4305-8bce-3d6db9ca87d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "67f43cde-9fe0-4e89-9b2e-617010b90a89");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_AspNetUsers_ReceiverId",
                table: "Recommendations",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_AspNetUsers_SenderId",
                table: "Recommendations",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_AspNetUsers_ReceiverId",
                table: "Recommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_AspNetUsers_SenderId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "DateGiven",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "RequestMessage",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Recommendations");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Recommendations",
                newName: "ReceivedByUserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Recommendations",
                newName: "GivenByUserId");

            migrationBuilder.RenameColumn(
                name: "DateRequested",
                table: "Recommendations",
                newName: "GivenAt");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_SenderId",
                table: "Recommendations",
                newName: "IX_Recommendations_ReceivedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_ReceiverId",
                table: "Recommendations",
                newName: "IX_Recommendations_GivenByUserId");

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
                name: "FK_Recommendations_AspNetUsers_GivenByUserId",
                table: "Recommendations",
                column: "GivenByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_AspNetUsers_ReceivedByUserId",
                table: "Recommendations",
                column: "ReceivedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
