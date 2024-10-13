using NullamWebApp.Shared.Enums;

namespace NullamWebApp.ApiService.Models.Response;

public class ParticipantCompanyResponse : ParticipantCompanyDto
{
	public Guid Id { get; set; }
	public PaymentType PaymentType { get; set; }
}
