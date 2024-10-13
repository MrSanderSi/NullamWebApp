namespace NullamWebApp.Web.Models.ApiResponseModels;

public class EventResponseWithParticipants
{
	public EventResponse? Event { get; set; }

	public List<ParticipantPerson>? ParticipantPeople { get; set; }
	public List<ParticipantCompany>? ParticipantCompanies { get; set; }

	public bool IsSuccess { get; set; }
	public string? ResponseMessage { get; set; }

	public bool? CanModifyParticipants { get; set; }
}
