using System.ComponentModel.DataAnnotations;

namespace NullamWebApp.ApiService.Models;

public class ParticipantCompanyDto
{
    public required string CompanyName { get; set; }
    public required int CompanyRegistryCode { get; set; }
    public required int AmountOfParticipants { get; set; }

    [MaxLength(5000)]
    public string? AdditionalInfo { get; set; }
}
