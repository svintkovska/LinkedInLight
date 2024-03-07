using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class UpdateChatAndMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletedForMe",
                table: "Messages",
                newName: "IsDeletedForSender");

            migrationBuilder.RenameColumn(
                name: "DeletedForAll",
                table: "Messages",
                newName: "IsDeletedForReceiver");

            migrationBuilder.RenameColumn(
                name: "DeletedForMe",
                table: "Chats",
                newName: "IsDeletedForParticipant2");

            migrationBuilder.RenameColumn(
                name: "DeletedForAll",
                table: "Chats",
                newName: "IsDeletedForParticipant1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a0952981-9dab-4eef-a350-ac32db0243f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "29f9158c-75d4-40c2-8eb6-2692a8ddf37e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "64ada3e1-77a0-4688-b07b-dfff5d6a07ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "afd68a7e-8796-4892-8a8a-89ac4f08ce0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "edad52dc-24d2-42e4-9969-8f64cc2f8e12");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "0d9d0d50-a637-4736-89a1-02fddabb0320");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeletedForSender",
                table: "Messages",
                newName: "DeletedForMe");

            migrationBuilder.RenameColumn(
                name: "IsDeletedForReceiver",
                table: "Messages",
                newName: "DeletedForAll");

            migrationBuilder.RenameColumn(
                name: "IsDeletedForParticipant2",
                table: "Chats",
                newName: "DeletedForMe");

            migrationBuilder.RenameColumn(
                name: "IsDeletedForParticipant1",
                table: "Chats",
                newName: "DeletedForAll");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b6e1c9e5-487b-4995-ae99-5583090e1ce2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6789b0f5-4de4-424d-951f-a8b8ea6b661b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "0df1d749-e900-48a0-bf45-4dd3bd029416");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "35829cec-89dd-4a09-aa67-e27ca57fad51");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "c5320cdd-fe97-4213-ae0a-b0e1a213a567");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "17492167-b53b-4ea4-95b4-02569326c310");
        }
    }
}
