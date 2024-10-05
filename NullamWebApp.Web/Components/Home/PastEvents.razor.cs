using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models.ApiResponseModels;

namespace NullamWebApp.Web.Components.Home;

public partial class PastEvents
{
	[Parameter]
	public List<EventResponse> Events { get; set; }
}
