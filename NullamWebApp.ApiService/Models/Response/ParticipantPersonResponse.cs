using NullamWebApp.Shared.Enums;

namespace NullamWebApp.ApiService.Models.Response
{
	public class ParticipantPersonResponse : ParticipantPersonDto
	{
		public Guid Id { get; set; }
		public PaymentType PaymentType { get; set; }
	}
}
