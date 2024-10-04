namespace NullamWebApp.ApiService.Models.Request;

public class EditParticipantRequest
{
    public Guid Id { get; set; }
    public ParticipantPersonDto? PersonDto { get; set; }
    public ParticipantCompanyDto? CompanyDto { get; set; }
}
