using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class RemoveRequiredFieldFromUser : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "About",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");

			migrationBuilder.AlterColumn<string>(
				name: "Country",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "About",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Country",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);
		}
	}
}
