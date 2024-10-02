namespace NullamWebApp.ApiService.Models.Response;

public class AllParticipantsResponse
{
    public List<ParticipantPersonDto> ParticipantPeople { get; set; } = new List<ParticipantPersonDto>();
    public List<ParticipantCompanyDto> ParticipantCompanys { get; set; } = new List<ParticipantCompanyDto>();

    public string? ResponseMessage { get; set; }
    public bool IsSuccess { get; set; } = true;
}
