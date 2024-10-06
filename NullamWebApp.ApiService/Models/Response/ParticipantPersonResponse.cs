using NullamWebApp.Shared.Enums;

namespace NullamWebApp.ApiService.Models.Response
{
	public class ParticipantPersonResponse : ParticipantPersonDto
	{
		public PaymentType PaymentType { get; set; }
	}
}
