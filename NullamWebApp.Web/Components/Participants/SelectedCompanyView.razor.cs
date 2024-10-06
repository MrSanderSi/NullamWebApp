using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models;

namespace NullamWebApp.Web.Components.Participants;

public partial class SelectedCompanyView
{
	[CascadingParameter]
	public SelectedCompany SelectedCompany { get; set; }
}
