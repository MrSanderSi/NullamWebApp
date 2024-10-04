using NullamWebApp.Shared.Models;

namespace NullamWebApp.ApiService.Models.Request;

public class AddEventRequest
{
    public required string EventName { get; set; }
    public required DateTimeOffset EventStart { get; set; }
    public required DateTimeOffset EventEnd { get; set; }
    public Address? Address { get; set; }
    public bool IsOnline { get; set; }
    public string? AdditionalInfo { get; set; }
}
