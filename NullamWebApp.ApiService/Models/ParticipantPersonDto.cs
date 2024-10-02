using System.ComponentModel.DataAnnotations;

namespace NullamWebApp.ApiService.Models;

public class ParticipantPersonDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required long IdCode { get; set; }

    [MaxLength(1500)]
    public string? AdditionalInfo { get; set; }
}
