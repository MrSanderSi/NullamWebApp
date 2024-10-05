using Microsoft.AspNetCore.Components;
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

    public async Task<ApiResponseMessage> DeleteEventAsync(Guid id)
    {
        return await _apiClient.DeleteEventAsync(id);
    }
}
