using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class AddJobAndQuestionModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecruiter",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RecruiterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndustryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPostings_AspNetUsers_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPostings_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobPostingId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_AspNetUsers_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobApplications_JobPostings_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreeningQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobPostingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningQuestions_JobPostings_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreeningAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreeningQuestionId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningAnswers_AspNetUsers_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScreeningAnswers_ScreeningQuestions_ScreeningQuestionId",
                        column: x => x.ScreeningQuestionId,
                        principalTable: "ScreeningQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e63d5663-cdea-4697-9373-8899297b7c05");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "fbf7c2b6-59ad-4b88-ad72-6b5e3a451d6d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "db958eec-2aa4-4bbf-bac5-4d1552b0cd6a");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CandidateId",
                table: "JobApplications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobPostingId",
                table: "JobApplications",
                column: "JobPostingId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_CompanyId",
                table: "JobPostings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_IndustryId",
                table: "JobPostings",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_RecruiterId",
                table: "JobPostings",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningAnswers_CandidateId",
                table: "ScreeningAnswers",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningAnswers_ScreeningQuestionId",
                table: "ScreeningAnswers",
                column: "ScreeningQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningQuestions_JobPostingId",
                table: "ScreeningQuestions",
                column: "JobPostingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "ScreeningAnswers");

            migrationBuilder.DropTable(
                name: "ScreeningQuestions");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropColumn(
                name: "IsRecruiter",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "17e3482d-5d38-4614-a213-3b3c12f8f613");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "df11fccf-2499-4fa4-8c0a-f96109e8f722");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e9a19394-0084-4aa7-8a0f-ce7bee21c0ce");
        }
    }
}
