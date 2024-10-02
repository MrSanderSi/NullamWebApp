using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NullamWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MissingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantCompanyId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantPersonId",
                table: "Participants");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ParticipantCompanyId",
                table: "Participants",
                column: "ParticipantCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ParticipantPersonId",
                table: "Participants",
                column: "ParticipantPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantCompanyId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantPersonId",
                table: "Participants");

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
        }
    }
}
