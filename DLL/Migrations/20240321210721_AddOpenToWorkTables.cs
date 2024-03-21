using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class AddOpenToWorkTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenToWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanStartImmediately = table.Column<bool>(type: "bit", nullable: false),
                    FullTime = table.Column<bool>(type: "bit", nullable: false),
                    PartTime = table.Column<bool>(type: "bit", nullable: false),
                    Internship = table.Column<bool>(type: "bit", nullable: false),
                    Contract = table.Column<bool>(type: "bit", nullable: false),
                    Temporary = table.Column<bool>(type: "bit", nullable: false),
                    VisibleForAll = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenToWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenToWorks_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenToWorkCities",
                columns: table => new
                {
                    OpenToWorkId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenToWorkCities", x => new { x.OpenToWorkId, x.CityId });
                    table.ForeignKey(
                        name: "FK_OpenToWorkCities_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpenToWorkCities_OpenToWorks_OpenToWorkId",
                        column: x => x.OpenToWorkId,
                        principalTable: "OpenToWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenToWorkCountries",
                columns: table => new
                {
                    OpenToWorkId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenToWorkCountries", x => new { x.OpenToWorkId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_OpenToWorkCountries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpenToWorkCountries_OpenToWorks_OpenToWorkId",
                        column: x => x.OpenToWorkId,
                        principalTable: "OpenToWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenToWorkPositions",
                columns: table => new
                {
                    OpenToWorkId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenToWorkPositions", x => new { x.OpenToWorkId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_OpenToWorkPositions_OpenToWorks_OpenToWorkId",
                        column: x => x.OpenToWorkId,
                        principalTable: "OpenToWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpenToWorkPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OpenToWorkCities_CityId",
                table: "OpenToWorkCities",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenToWorkCountries_CountryId",
                table: "OpenToWorkCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenToWorkPositions_PositionId",
                table: "OpenToWorkPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenToWorks_ApplicationUserId",
                table: "OpenToWorks",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenToWorkCities");

            migrationBuilder.DropTable(
                name: "OpenToWorkCountries");

            migrationBuilder.DropTable(
                name: "OpenToWorkPositions");

            migrationBuilder.DropTable(
                name: "OpenToWorks");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "70d7f269-1de5-42ba-bac2-7d26a4979563");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "fa6eb088-3d6b-4dc9-91db-52108dd70d9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "5fb886ae-d82e-4c79-badf-15b156397009");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "5fea3eb7-62cf-4e73-826d-d907c42e0799");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "667dde92-29b8-4ab6-afdd-beb9e217ca70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "9785ecac-e89d-488e-82a0-2bc22bf6471c");
        }
    }
}
