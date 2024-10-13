using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models.ApiResponseModels;
using NullamWebApp.Web.Services;

namespace NullamWebApp.Web.Components.Pages;

public partial class Home
{
    [Inject]
    public EventService EventService { get; set; }

    public List<EventResponse> UpcomingEvents { get; set; }

    public List<EventResponse> PastEvents { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UpcomingEvents = await EventService.GetAllUpcomingEventsAsync();
        PastEvents = await EventService.GetAllPastEventsAsync();
    }
}
