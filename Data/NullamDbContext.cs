using Microsoft.EntityFrameworkCore;
using NullamWebApp.Data.Models;

namespace NullamWebApp.Data;

public class NullamDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<ParticipantEntry> Participants { get; set; }
    public DbSet<ParticipantPerson> ParticipantPeople { get; set; }
    public DbSet<ParticipantCompany> ParticipantCompanies { get; set; }

    private static string UpcomingEventId = "A1B2C3D4-E5F6-7890-1234-56789ABCDEF0";
    private static string PastEventId = "0987FEDC-BA65-4321-0FED-CBA987654321";
    private static string TestTesterId = "F1E2D3C4-B5A6-7890-1234-56789ABCDEF0";
    private static string TestCompanyId = "1234ABCD-5678-EF90-1234-56789FEDCBA0";
    private static string TestAddressId = "ABCDEF12-3456-7890-ABCD-EF1234567890";

	public NullamDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParticipantPerson>()
            .HasMany(x => x.ParticipantEntry)
            .WithOne(x => x.Person)
            .IsRequired(false);

        modelBuilder.Entity<ParticipantCompany>()
            .HasMany(x => x.ParticipantEntry)
            .WithOne(x => x.Company)
            .IsRequired(false);

        modelBuilder.Entity<ParticipantEntry>()
            .HasOne(x => x.Event)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.EventId);

        // Seed some initial data
        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = Guid.Parse(UpcomingEventId),
                EventName = "Upcoming Test Event",
                EventStarts = new DateTimeOffset(2024, 11, 20, 10, 0, 0, TimeSpan.Zero),
                EventEnds = new DateTimeOffset(2024, 11, 20, 12, 0, 0, TimeSpan.Zero),
                IsOnline = false,
                AdditionalInfo = "Info kohale tulijatele"
            },
            new Event
            {
                Id = Guid.Parse(PastEventId),
                EventName = "Past Test Event",
                EventStarts = new DateTimeOffset(2024, 10, 10, 10, 0, 0, TimeSpan.Zero),
                EventEnds = new DateTimeOffset(2024, 10, 10, 12, 0, 0, TimeSpan.Zero),
                IsOnline = false,
                AdditionalInfo = "Info kohale tulijatele"
            });

        modelBuilder.Entity<ParticipantPerson>().HasData(
            new ParticipantPerson
            {
                Id = Guid.Parse(TestTesterId),
                FirstName = "Test",
                LastName = "Tester",
                IdCode = 35204096585
            });

        modelBuilder.Entity<ParticipantCompany>().HasData(
            new ParticipantCompany
            {
                Id = Guid.Parse(TestCompanyId),
                CompanyName = "Test Company OÜ",
                RegistryCode = 12400621,
            });

        modelBuilder.Entity<ParticipantEntry>().HasData(
            new ParticipantEntry
            {
                Id = Guid.NewGuid(),
                EventId = Guid.Parse(PastEventId),
                ParticipantPersonId = Guid.Parse(TestTesterId),
                ParticipantCount = 1,
                PaymentType = Shared.Enums.PaymentType.Cash,
            },
            new ParticipantEntry
            {
                Id = Guid.NewGuid(),
                EventId = Guid.Parse(PastEventId),
                ParticipantCompanyId = Guid.Parse(TestCompanyId),
                ParticipantCount = 5,
                PaymentType = Shared.Enums.PaymentType.BankTransfer,
            });
    }
}
