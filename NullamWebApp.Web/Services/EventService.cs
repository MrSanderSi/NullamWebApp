using Microsoft.AspNetCore.Components;
using NullamWebApp.Web.Models.ApiRequestModels;
using NullamWebApp.Web.Models.ApiResponseModels;

namespace NullamWebApp.Web.Services;

public class EventService
{
    private readonly NullamApiClient _apiClient;

    public EventService(NullamApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<EventResponse>> GetAllUpcomingEventsAsync()
    {
        return await _apiClient.GetUpcomingEventListAsync();
    }

    public async Task<List<EventResponse>> GetAllPastEventsAsync()
    {
        return await _apiClient.GetPastEventListAsync();
    }

    public async Task<EventResponseWithParticipants> GetEventWithParticipantsAsync(Guid id)
    {
        var result = await _apiClient.GetEventWithParticipantsAsync(id);

        if (result.IsSuccess)
        {
            result.CanModifyParticipants = result?.Event?.EventStart > DateTimeOffset.Now;
        }

        return result;
	}

    public async Task<ApiResponseMessage> AddEventAsync(AddEventRequest request)
    {
        return await _apiClient.AddEventAsync(request);
    }


	public async Task<ApiResponseMessage> AddOrEditParticipantInEventAsync(AddParticipantRequest request)
    {
        return await _apiClient.AddParticipantInEventAsync(request);
    }

	public async Task<ApiResponseMessage> DeleteEventAsync(Guid id)
    {
        return await _apiClient.DeleteEventAsync(id);
    }

    public async Task<ApiResponseMessage> DeleteParticipantPersonFromEvent(Guid id, ParticipantPerson person)
    {
        var request = new DeleteParticipantRequest()
        {
            ParticipantId = (Guid)person.Id,
            EventId = id,
            ParticipantPerson = person
        };

        return await _apiClient.DeleteParticipantFromEventAsync(request);
    }

	public async Task<ApiResponseMessage> DeleteParticipantCompanyFromEvent(Guid id, ParticipantCompany company)
	{
		var request = new DeleteParticipantRequest()
		{
			ParticipantId = (Guid)company.Id,
			EventId = id,
			ParticipantCompany = company
		};

		return await _apiClient.DeleteParticipantFromEventAsync(request);
	}
}
