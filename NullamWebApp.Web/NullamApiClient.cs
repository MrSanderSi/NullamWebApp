using NullamWebApp.Web.Models.ApiResponseModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NullamWebApp.Web;

public class NullamApiClient(HttpClient httpClient)
{
    public async Task<List<EventResponse>> GetUpcomingEventListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/api/Events/All/Upcoming");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(responseBody))
        {
            var events = JsonSerializer.Deserialize<ApiListResponse<EventResponse>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return events?.Response ?? new List<EventResponse>();
        }

        return new List<EventResponse>();
    }

    public async Task<List<EventResponse>> GetPastEventListAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("/api/Events/All/Past");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(responseBody))
        {
            var events = JsonSerializer.Deserialize<ApiListResponse<EventResponse>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return events?.Response ?? new List<EventResponse>();
        }

        return new List<EventResponse>();
    }

    public async Task<EventResponseWithParticipants> GetEventWithParticipantsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/api/Events/WithParticipants/{id}");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(responseBody))
        {
            var eventWithParticipants = JsonSerializer.Deserialize<EventResponseWithParticipants>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return eventWithParticipants ?? new EventResponseWithParticipants() { IsSuccess = false, ResponseMessage = "Failed to receive given event with participants" };
        }

        return new EventResponseWithParticipants() { IsSuccess = false, ResponseMessage = "Failed to receive given event with participants" };
    }

    public async Task<ApiResponseMessage> DeleteEventAsync(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Events/{id}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(responseBody))
        {
            var responseMessage = JsonSerializer.Deserialize<ApiResponseMessage>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return responseMessage ?? new ApiResponseMessage() { IsSuccess = false, ResultMessage = "Failed to delete given event" };
        }

        return new ApiResponseMessage() { IsSuccess = false, ResultMessage = "Failed to delete given event" };
    }

    public async Task<ApiResponseMessage> DeleteParticipantAsync(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Participants/{id}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrWhiteSpace(responseBody))
        {
            var responseMessage = JsonSerializer.Deserialize<ApiResponseMessage>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return responseMessage ?? new ApiResponseMessage() { IsSuccess = false, ResultMessage = "Failed to delete given participant" };
        }

        return new ApiResponseMessage() { IsSuccess = false, ResultMessage = "Failed to delete given participant" };
    }
}