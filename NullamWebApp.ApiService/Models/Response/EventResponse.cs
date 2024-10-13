using NullamWebApp.Shared.Models;

namespace NullamWebApp.ApiService.Models.Response;

public class EventResponse
{
    public Guid Id { get; set; }
    public required string EventName { get; set; }
    public DateTimeOffset EventStart { get; set; }
    public DateTimeOffset EventEnd { get; set; }
    public Address? Address { get; set; }
    public int ParticipantCount { get; set; }
    public bool IsOnline { get; set; }
    public string? AdditionalInfo { get; set; }
}

public class SingleEventResponse
{
    public EventResponse? EventResponse { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string? ResponseMessage { get; set; }
}