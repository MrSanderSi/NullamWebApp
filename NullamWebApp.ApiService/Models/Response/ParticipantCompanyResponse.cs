using NullamWebApp.Shared.Enums;

namespace NullamWebApp.ApiService.Models.Response;

public class ParticipantCompanyResponse : ParticipantCompanyDto
{
	public PaymentType PaymentType { get; set; }
}
