using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Sections_SectionModelId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Surveys_SurveyModelId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_SurveyModelId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SectionModelId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionOptions_QuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "SurveyModelId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "SectionModelId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "QuestionOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_SectionId",
                table: "Surveys",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Sections_SectionId",
                table: "Surveys",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Sections_SectionId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_SectionId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "QuestionOptions");

            migrationBuilder.AddColumn<int>(
                name: "SurveyModelId",
                table: "Sections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectionModelId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionModelId",
                table: "QuestionOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SurveyModelId",
                table: "Sections",
                column: "SurveyModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SectionModelId",
                table: "Questions",
                column: "SectionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionModelId",
                table: "QuestionOptions",
                column: "QuestionModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionModelId",
                table: "QuestionOptions",
                column: "QuestionModelId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Sections_SectionModelId",
                table: "Questions",
                column: "SectionModelId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Surveys_SurveyModelId",
                table: "Sections",
                column: "SurveyModelId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }
    }
}
