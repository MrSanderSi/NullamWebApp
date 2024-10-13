namespace NullamWebApp.Web.Models;

public class NewEvent
{
	public string EventName { get; set; }
	public DateTimeOffset StartDateTime { get; set; } = DateTimeOffset.UtcNow;
	public string Address { get; set; }
	public string AdditionalInfo { get; set; }
}
