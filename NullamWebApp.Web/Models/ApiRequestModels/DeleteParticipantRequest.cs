using NullamWebApp.Web.Models.ApiResponseModels;

namespace NullamWebApp.Web.Models.ApiRequestModels
{
	public class DeleteParticipantRequest
	{
		public Guid ParticipantId { get; set; }
		public Guid EventId { get; set; }
		public ParticipantPerson? ParticipantPerson { get; set; }
		public ParticipantCompany? ParticipantCompany { get; set; }
	}
}
