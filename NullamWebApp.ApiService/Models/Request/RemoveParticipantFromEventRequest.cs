namespace NullamWebApp.ApiService.Models.Request;

public class RemoveParticipantFromEventRequest
{
	public Guid ParticipantId { get; set; }
	public Guid EventId { get; set; }
	public ParticipantPersonDto? ParticipantPerson { get; set; }
	public ParticipantCompanyDto? ParticipantCompany { get; set; }
}
