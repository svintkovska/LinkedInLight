using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class AddUserPrivacySettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPrivacySettings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileViewingValue = table.Column<int>(type: "int", nullable: false),
                    EmailVisibilityValue = table.Column<int>(type: "int", nullable: false),
                    ConnectionVisibility = table.Column<bool>(type: "bit", nullable: false),
                    ShowLastName = table.Column<bool>(type: "bit", nullable: false),
                    DiscoverByEmailValue = table.Column<int>(type: "int", nullable: false),
                    DiscoverByPhoneValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrivacySettings", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserPrivacySettings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bbbf4755-6ec5-4522-b318-b41de92f046f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d7269172-78db-45ef-a103-ef099b7132f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "81ab31d7-1112-4853-9727-a460961ec428");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "24dca43d-a3a2-47d2-a9f3-6493a7f27874");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "303fe097-ad94-435a-8e06-e98147aac1af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "f0ac50f0-6ed3-4dac-87b6-608dd47f7949");

            migrationBuilder.AddColumn<string>(
                name: "ProfileURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPrivacySettings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0d5ec66a-1057-4867-a991-252fb10c198c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f3012280-c7df-4622-9981-20efe1ce751a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "80c86c9f-8029-4074-9a7d-61fb5b620fa3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "8593d739-f4b7-40d3-88c9-07b93bb11409");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "70412a45-528a-4071-bcb7-30b80fe42053");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "ddac32c5-3030-4eb5-8918-bd4041e547d7");

            migrationBuilder.DropColumn(
                name: "ProfileURL",
                table: "AspNetUsers");
        }
    }
}
