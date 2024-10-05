using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models.ApiResponseModels;
using NullamWebApp.Web.Services;

namespace NullamWebApp.Web.Components.Home;

public partial class EventList
{
    [Inject]
    public EventService EventService { get; set; }

    [Parameter]
    public required List<EventResponse> Events { get; set; }

    [Parameter]
    public bool CanDelete {  get; set; }

    [Parameter]
    public required string Title { get; set; }

	protected override Task OnParametersSetAsync()
	{
		return base.OnParametersSetAsync();
	}

	public async Task RemoveEventAsync(Guid id)
    {
        var response = await EventService.DeleteEventAsync(id);

        if (response.IsSuccess)
        {
            Events.Remove(Events.Where(x => x.Id == id).First());
        }

        StateHasChanged();
    }
}
