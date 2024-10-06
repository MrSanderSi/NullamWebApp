namespace NullamWebApp.ApiService.Models.Response;

public class EventWithParticipantsResponse
{
	public EventResponse? Event { get; set; }
	public List<ParticipantPersonResponse> ParticipantPeople { get; set; } = new List<ParticipantPersonResponse>();
	public List<ParticipantCompanyResponse> ParticipantCompanys { get; set; } = new List<ParticipantCompanyResponse>();

	public bool IsSuccess { get; set; } = true;
	public string? ResponseMessage { get; set; }
}
