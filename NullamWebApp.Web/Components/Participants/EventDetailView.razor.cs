using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models;
using NullamWebApp.Web.Models.ApiResponseModels;
using NullamWebApp.Web.Services;

namespace NullamWebApp.Web.Components.Participants;

public partial class EventDetailView : ComponentBase
{
	[Inject]
	public EventService EventService { get; set; }

	[Inject]
	public ParticipantService ParticipantService { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Parameter]
	public string EventId { get; set; }

	private bool CanModify { get; set; }

	public EventResponseWithParticipants EventWithParticipants { get; set; }
	public bool ParticipantViewActive { get; set; }

	public SelectedPerson? SelectedPerson { get; set; }
	public SelectedCompany? SelectedCompany { get; set; }

	public UserType SelectedOption { get; set; } = UserType.Person;

	protected override async Task OnParametersSetAsync()
	{
		if(Guid.TryParse(EventId, out Guid eventId))
		{
			var response = await EventService.GetEventWithParticipantsAsync(eventId);

			if (response.IsSuccess)
			{
				EventWithParticipants = response;
				CanModify = response.CanModifyParticipants ?? false;
			}
		}

		StateHasChanged();
	}

	public async Task DeleteParticipantPersonAsync(ParticipantPerson person)
	{
		var result = await EventService.DeleteParticipantPersonFromEvent(Guid.Parse(EventId), person);

		if (result.IsSuccess)
		{
			StateHasChanged();
		}
	}

	public async Task DeleteParticipantCompanyAsync(ParticipantCompany company)
	{
		var result = await EventService.DeleteParticipantCompanyFromEvent(Guid.Parse(EventId), company);

		if (result.IsSuccess)
		{
			StateHasChanged();
		}
	}

	public void ChoosePerson()
	{
		SelectedOption = UserType.Person;
		SelectedPerson = new SelectedPerson() { FirstName = string.Empty, LastName = string.Empty, IdCode = int.MaxValue };
		StateHasChanged();
	}

	public void ChooseCompany()
	{
		SelectedOption = UserType.Company;
		SelectedCompany = new SelectedCompany() { CompanyName = string.Empty, CompanyRegistryCode = int.MaxValue, AmountOfParticipants = int.MaxValue };
		StateHasChanged();
	}

	public void TriggerSelectedPerson(ParticipantPerson person)
	{
		SelectedPerson = new SelectedPerson()
		{
			FirstName = person.FirstName,
			LastName = person.LastName,
			IdCode = person.IdCode,
			AdditionalInfo = person.AdditionalInfo,
			PaymentType = person.PaymentType
		};

		if (Guid.TryParse(EventId, out Guid eventId))
		{
			SelectedPerson.EventId = eventId;
		}

		SelectedCompany = null;
		ParticipantViewActive = true;

		StateHasChanged();
	}

	public void TriggerSelectedCompany(ParticipantCompany company)
	{

		SelectedCompany = new SelectedCompany()
		{
			AmountOfParticipants = company.AmountOfParticipants,
			CompanyName = company.CompanyName,
			CompanyRegistryCode = company.CompanyRegistryCode,
			AdditionalInfo = company.AdditionalInfo,
			PaymentType = company.PaymentType,
		};

		if (Guid.TryParse(EventId, out Guid eventId))
		{
			SelectedCompany.EventId = eventId;
		}

		SelectedPerson = null;
		ParticipantViewActive = true;

		StateHasChanged();
	}
}

public enum UserType
{
	Person = 0,
	Company = 1
}
