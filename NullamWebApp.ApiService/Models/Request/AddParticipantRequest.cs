using NullamWebApp.Shared.Enums;

namespace NullamWebApp.ApiService.Models.Request;

public class AddParticipantRequest
{
    public Guid? Id { get; set; }
    public required Guid EventId { get; set; }

    public PaymentType PaymentType { get; set; }

    public ParticipantPersonDto? PersonDto { get; set; }
    public ParticipantCompanyDto? CompanyDto { get; set; }
}
