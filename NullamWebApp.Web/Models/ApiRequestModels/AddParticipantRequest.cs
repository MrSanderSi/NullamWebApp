using NullamWebApp.Shared.Enums;
using NullamWebApp.Web.Models.ApiResponseModels;

namespace NullamWebApp.Web.Models.ApiRequestModels;

public class AddParticipantRequest
{
	public Guid? Id { get; set; }
	public required Guid EventId { get; set; }

	public PaymentType PaymentType { get; set; }

	public ParticipantPerson? PersonDto { get; set; }
	public ParticipantCompany? CompanyDto { get; set; }
}