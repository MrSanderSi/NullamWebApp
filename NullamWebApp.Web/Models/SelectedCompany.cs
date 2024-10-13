using NullamWebApp.Shared.Enums;
using NullamWebApp.Web.Models.ApiResponseModels;

namespace NullamWebApp.Web.Models;

public class SelectedCompany : ParticipantCompany
{
	public PaymentType PaymentType { get; set; }
	public Guid EventId { get; set; }
}
