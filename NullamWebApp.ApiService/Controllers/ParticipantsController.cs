using Microsoft.AspNetCore.Mvc;
using NullamWebApp.ApiService.Models;
using NullamWebApp.ApiService.Models.Request;
using NullamWebApp.ApiService.Models.Response;
using NullamWebApp.ApiService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NullamWebApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly AddParticipantService _participantService;

        public ParticipantsController(AddParticipantService participantService)
        {
            _participantService = participantService;
        }

        // GET api/<ParticipantsController>/5
        [HttpGet("{eventId}")]
        public async Task<AllParticipantsResponse> Get(Guid eventId)
        {
            return await _participantService.GetAllParticipantsFromEventAsync(eventId);
        }

        // POST api/<ParticipantsController>
        [HttpPost]
        public async Task<ApiResponseMessage> PostAsync(AddParticipantRequest participant)
        {
            return await _participantService.AddParticipantToEventAsync(participant);
        }

        [HttpPut]
        public async Task<ApiResponseMessage> EditParticipantAsync(EditParticipantRequest participant)
        {
            return await _participantService.EditParticipantAsync(participant);
        }

        // DELETE api/<ParticipantsController>/5
        [HttpDelete("{id}")]
        public async Task<ApiResponseMessage> Delete(Guid id)
        {
            return await _participantService.RemoveParticipantAsync(new RemoveParticipantRequest { Id = id });
        }
    }
}
