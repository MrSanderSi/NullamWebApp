using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models;

namespace NullamWebApp.Web.Components.Participants;

public partial class SelectedPersonView
{
	[CascadingParameter]
	public SelectedPerson SelectedPerson { get; set; }
}
