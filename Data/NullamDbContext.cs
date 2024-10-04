using Microsoft.EntityFrameworkCore;
using NullamWebApp.Data.Models;

namespace NullamWebApp.Data;

public class NullamDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<ParticipantEntry> Participants { get; set; }
    public DbSet<ParticipantPerson> ParticipantPeople { get; set; }
    public DbSet<ParticipantCompany> ParticipantCompanies { get; set; }

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
    }
}
