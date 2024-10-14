using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NullamWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataPatch : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
			table: "ParticipantPeople",
			columns: new[] { "Id", "FirstName", "IdCode", "LastName" },
			values: new object[] { new Guid("f1e2d3c4-b5a6-7890-1234-56789abcdef0"), "Test", 35204096585L, "Tester" });

			migrationBuilder.InsertData(
			table: "ParticipantCompanies",
			columns: new[] { "Id", "CompanyName", "RegistryCode" },
			values: new object[] { new Guid("1234abcd-5678-ef90-1234-56789fedcba0"), "Test Company OÜ", 12400621 });

			migrationBuilder.InsertData(
			table: "Events",
			columns: new[] { "Id", "AdditionalInfo", "AddressId", "EventEnds", "EventName", "EventStarts", "IsOnline" },
			values: new object[,]
			{
				{ new Guid("0987fedc-ba65-4321-0fed-cba987654321"), "Info kohale tulijatele", null, new DateTimeOffset(new DateTime(2024, 10, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Past Test Event", new DateTimeOffset(new DateTime(2024, 10, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false },
				{ new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"), "Info kohale tulijatele", null, new DateTimeOffset(new DateTime(2024, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Upcoming Test Event", new DateTimeOffset(new DateTime(2024, 11, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false }
			});

			migrationBuilder.InsertData(
			table: "Participants",
			columns: new[] { "Id", "AdditionalInfo", "EventId", "ParticipantCompanyId", "ParticipantCount", "ParticipantPersonId", "PaymentType" },
			values: new object[,]
			{
				{ new Guid("0e594b0a-aa13-47b0-bf35-cc641b0554e9"), null, new Guid("0987fedc-ba65-4321-0fed-cba987654321"), new Guid("1234abcd-5678-ef90-1234-56789fedcba0"), 5, null, 0 },
				{ new Guid("664ab5f1-7b10-4510-82c4-9e75626f89d4"), null, new Guid("0987fedc-ba65-4321-0fed-cba987654321"), null, 1, new Guid("f1e2d3c4-b5a6-7890-1234-56789abcdef0"), 1 }
			});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
			table: "Participants",
			keyColumn: "Id",
			keyValue: new Guid("0e594b0a-aa13-47b0-bf35-cc641b0554e9"));

			migrationBuilder.DeleteData(
			table: "Participants",
			keyColumn: "Id",
			keyValue: new Guid("664ab5f1-7b10-4510-82c4-9e75626f89d4"));

			migrationBuilder.DeleteData(
			table: "Events",
			keyColumn: "Id",
			keyValue: new Guid("0987fedc-ba65-4321-0fed-cba987654321"));

			migrationBuilder.DeleteData(
			table: "Events",
			keyColumn: "Id",
			keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"));

			migrationBuilder.DeleteData(
			table: "ParticipantCompanies",
			keyColumn: "Id",
			keyValue: new Guid("1234abcd-5678-ef90-1234-56789fedcba0"));

			migrationBuilder.DeleteData(
			table: "ParticipantPeople",
			keyColumn: "Id",
			keyValue: new Guid("f1e2d3c4-b5a6-7890-1234-56789abcdef0"));
		}
	}
}
