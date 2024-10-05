using Microsoft.AspNetCore.Mvc;
using NullamWebApp.ApiService.Models.Request;
using NullamWebApp.ApiService.Models.Response;
using NullamWebApp.ApiService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NullamWebApp.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly AddEventService _eventService;
    public EventsController(AddEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("{id}")]
    public async Task<SingleEventResponse> GetAsync(Guid id)
    {
        return await _eventService.GetEventAsync(new GetEventRequest() { Id = id });
    }

    [HttpGet("All/Upcoming")]
    public async Task<IActionResult> GetAllUpcoming()
    {
        var result = await _eventService.GetAllUpcomingEvents();
        
        if (result.Response.Count > 0)
        {
            return Ok(result);
        }

        return NoContent();
    }


    [HttpGet("All/Past")]
    public async Task<IActionResult> GetAllPast()
    {
        var result = await _eventService.GetAllPastEvents();

        if (result.Response.Count > 0)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ApiResponseMessage> PostAsync(AddEventRequest value)
    {
        return await _eventService.AddEventAsync(value);
    }

    [HttpPut]
    public async Task<ApiResponseMessage> EditEventAsync(EditEventRequest request)
    {
        return await _eventService.EditEventAsync(request);
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponseMessage> DeleteAsync(Guid id)
    {
        var request = new DeleteEventRequest() { Id = id };
        return await _eventService.DeleteEventAsync(request);
    }
}
