namespace NullamWebApp.ApiService.Models.Request;

public class RemoveParticipantRequest
{
    public Guid Id { get; set; }
    public bool IsCompany { get; set; }
}
