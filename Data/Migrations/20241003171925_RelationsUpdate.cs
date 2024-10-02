using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NullamWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantCompanies_Participants_Id",
                table: "ParticipantCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantPeople_Participants_Id",
                table: "ParticipantPeople");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ParticipantCompanyId",
                table: "Participants",
                column: "ParticipantCompanyId",
                unique: true,
                filter: "[ParticipantCompanyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ParticipantPersonId",
                table: "Participants",
                column: "ParticipantPersonId",
                unique: true,
                filter: "[ParticipantPersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_ParticipantCompanies_ParticipantCompanyId",
                table: "Participants",
                column: "ParticipantCompanyId",
                principalTable: "ParticipantCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_ParticipantPeople_ParticipantPersonId",
                table: "Participants",
                column: "ParticipantPersonId",
                principalTable: "ParticipantPeople",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_ParticipantCompanies_ParticipantCompanyId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_ParticipantPeople_ParticipantPersonId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantCompanyId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantPersonId",
                table: "Participants");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantCompanies_Participants_Id",
                table: "ParticipantCompanies",
                column: "Id",
                principalTable: "Participants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantPeople_Participants_Id",
                table: "ParticipantPeople",
                column: "Id",
                principalTable: "Participants",
                principalColumn: "Id");
        }
    }
}
