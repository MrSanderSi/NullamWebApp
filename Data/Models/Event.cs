using System.ComponentModel.DataAnnotations.Schema;

namespace NullamWebApp.Data.Models;

public class Event
{
    public Guid Id { get; set; }
    public required string EventName { get; set; }
    public DateTimeOffset EventStarts {  get; set; }
    public DateTimeOffset EventEnds { get; set; }
    public Address? Address { get; set; }
    public bool IsOnline { get; set; }
    public string? AdditionalInfo { get; set; }
    public IEnumerable<ParticipantEntry>? Participants { get; set; }
}
