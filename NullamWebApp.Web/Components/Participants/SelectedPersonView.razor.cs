using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models;
using NullamWebApp.Web.Models.ApiRequestModels;
using NullamWebApp.Web.Services;

namespace NullamWebApp.Web.Components.Participants;

public partial class SelectedPersonView
{
	[Inject]
	public EventService EventService { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[CascadingParameter]
	public SelectedPerson? SelectedPerson { get; set; }

	[CascadingParameter]
	public Guid EventId { get; set; }

	protected override Task OnInitializedAsync()
	{
		if(SelectedPerson == null)
		{
			SelectedPerson = new SelectedPerson() { FirstName = string.Empty, LastName = string.Empty, IdCode = 0 };
		}

		return base.OnInitializedAsync();
	}

	private async void SaveChangesAsync()
	{
		var participantToAdd = SelectedPerson;

		var addParticipantRequest = new AddParticipantRequest()
		{
			EventId = EventId,
			PersonDto = SelectedPerson,
			Id = SelectedPerson.Id,
			PaymentType = SelectedPerson.PaymentType
		};

		var result = await EventService.AddOrEditParticipantInEventAsync(addParticipantRequest);
	}

	private void NavigateBack()
	{
		NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
	}
}
