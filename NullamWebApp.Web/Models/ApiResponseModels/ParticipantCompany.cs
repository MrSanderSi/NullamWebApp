using NullamWebApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace NullamWebApp.Web.Models.ApiResponseModels;

public class ParticipantCompany
{
	public required string CompanyName { get; set; }
	public required int CompanyRegistryCode { get; set; }
	public required int AmountOfParticipants { get; set; }
	public PaymentType PaymentType { get; set; }

	[MaxLength(5000)]
	public string? AdditionalInfo { get; set; }
}