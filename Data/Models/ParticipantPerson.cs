using NullamWebApp.Shared.Enums;

namespace NullamWebApp.Data.Models;

public class ParticipantPerson
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long IdCode { get; set; }
    public virtual IEnumerable<ParticipantEntry>? ParticipantEntry { get; set; }
}
