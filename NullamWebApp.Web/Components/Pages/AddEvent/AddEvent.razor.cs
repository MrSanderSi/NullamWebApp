using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models;
using NullamWebApp.Web.Models.ApiRequestModels;
using NullamWebApp.Web.Services;

namespace NullamWebApp.Web.Components.Pages.AddEvent;

public partial class AddEvent
{
	[Inject]
	public EventService EventService { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	public NewEvent NewEvent { get; set; } = new NewEvent();

	private async void SaveChangesAsync()
	{
		var addEventRequest = new AddEventRequest()
		{
			EventName = NewEvent.EventName,
			EventStart = NewEvent.StartDateTime,
			// Currently unused
			EventEnd = NewEvent.StartDateTime.AddHours(8),
			Address = new Shared.Models.Address()
			{
				AdditionalLines = NewEvent.Address
			},
			IsOnline = string.IsNullOrWhiteSpace(NewEvent.Address),
			AdditionalInfo = NewEvent.AdditionalInfo
		};

		var result = await EventService.AddEventAsync(addEventRequest);

		if (result.IsSuccess)
		{
			NavigationManager.NavigateTo("/", forceLoad: true);
		}
	}

	private void NavigateBack()
	{
		NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
	}
}
