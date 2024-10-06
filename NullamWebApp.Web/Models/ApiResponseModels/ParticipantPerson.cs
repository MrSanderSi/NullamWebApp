using NullamWebApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace NullamWebApp.Web.Models.ApiResponseModels;

public class ParticipantPerson
{
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
	public required long IdCode { get; set; }
	public PaymentType PaymentType { get; set; }

	[MaxLength(1500)]
	public string? AdditionalInfo { get; set; }
}
