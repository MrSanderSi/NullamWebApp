namespace NullamWebApp.ApiService.Models.Request;

public class EditEventRequest : AddEventRequest
{
    public Guid EventId { get; set; }
}
