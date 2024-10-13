using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models;
using NullamWebApp.Web.Models.ApiRequestModels;
using NullamWebApp.Web.Services;

namespace NullamWebApp.Web.Components.Participants;

public partial class SelectedCompanyView
{
	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public EventService EventService { get; set; }

	[CascadingParameter]
	public SelectedCompany? SelectedCompany { get; set; }

	[CascadingParameter]
	public Guid EventId { get; set; }

	protected override Task OnInitializedAsync()
	{
		if(SelectedCompany == null)
		{
			SelectedCompany = new SelectedCompany() { CompanyName = string.Empty, CompanyRegistryCode = 0, AmountOfParticipants = 0 };
		}

		return base.OnInitializedAsync();
	}

	private async void SaveChangesAsync()
	{
		var participantToAdd = SelectedCompany;

		var addParticipantRequest = new AddParticipantRequest()
		{
			EventId = EventId,
			CompanyDto = SelectedCompany,
			Id = SelectedCompany.Id,
			PaymentType = SelectedCompany.PaymentType
		};

		var result = await EventService.AddOrEditParticipantInEventAsync(addParticipantRequest);
	}

	private void NavigateBack()
	{
		NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
	}
}
