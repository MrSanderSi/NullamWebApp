using Microsoft.EntityFrameworkCore;
using NullamWebApp.ApiService.Models;
using NullamWebApp.ApiService.Models.Request;
using NullamWebApp.ApiService.Models.Response;
using NullamWebApp.Data;
using NullamWebApp.Data.Models;

namespace NullamWebApp.ApiService.Services;

public class AddParticipantService : ServiceBase
{
    private readonly NullamDbContext _db;

    public AddParticipantService(NullamDbContext db)
    {
        _db = db;
    }

    public async Task<ApiResponseMessage> AddParticipantToEventAsync(AddParticipantRequest participant)
    {
        var givenEvent = await _db.Set<Event>().Where(x => x.Id == participant.EventId)
            .FirstOrDefaultAsync();

        if (givenEvent == null)
        {
            return new ApiResponseMessage("Failed to find given event to register to");
        }

        if (participant.CompanyDto != null)
        {
            return await AddCompanyParticipantToEventAsync(participant, givenEvent);
        }
        else if (participant.PersonDto != null)
        {
            return await AddPersonParticipantToEventAsync(participant, givenEvent);
        }

        return new ApiResponseMessage("Adding participant to given even failed.");
    }

    public async Task<ApiResponseMessage> EditParticipantAsync(EditParticipantRequest participant)
    {
        if (participant.CompanyDto != null)
        {
            return await EditCompanyParticipantAsync(participant.CompanyDto, participant.Id);
        }

        return await EditPersonParticipantAsync(participant.PersonDto, participant.Id);
    }

    public async Task<ApiResponseMessage> RemoveParticipantAsync(RemoveParticipantRequest request)
    {
        bool isSuccess;

        if (request.IsCompany)
        {
            isSuccess = await RemoveCompanyParticipantAsync(request.Id);
        }
        else
        {
            isSuccess = await RemovePersonParticipantAsync(request.Id);
        }

        if (isSuccess)
        {
            return new ApiResponseMessage("Participant has been remove from database.");
        }

        return new ApiResponseMessage(isSuccess: false, "Did not find given participant");
    }

    public async Task<AllParticipantsResponse> GetAllParticipantsFromEventAsync(Guid eventId)
    {
        var result = new AllParticipantsResponse();

        var connectedEvent = await _db.Set<Event>()
            .Include(x => x.Participants)
                .ThenInclude(x => x.Person)
            .Include(x => x.Participants)
                .ThenInclude(x => x.Company)
            .FirstOrDefaultAsync(x => x.Id == eventId);

        if(connectedEvent != null && connectedEvent.Participants != null)
        {
            foreach(var participant in connectedEvent.Participants)
            {
                if(participant.ParticipantPersonId != null)
                {
                    var person = participant.Person;

                    result.ParticipantPeople.Add(new ParticipantPersonDto()
                        {
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            IdCode = person.IdCode,
                            AdditionalInfo = participant.AdditionalInfo
                        });
                }
                else if(participant.ParticipantCompanyId != null)
                {
                    var company = participant.Company;
                    result.ParticipantCompanys.Add(new ParticipantCompanyDto()
                    {
                        CompanyName = company.CompanyName,
                        CompanyRegistryCode = company.RegistryCode,
                        AmountOfParticipants = participant.ParticipantCount,
                        AdditionalInfo = participant.AdditionalInfo,
                    });
                }
            }
        }

        return result;
    }

    private async Task<bool> RemovePersonParticipantAsync(Guid id)
    {
        var match = await _db.Set<ParticipantPerson>().FirstOrDefaultAsync(x => x.Id == id);

        if (match != null)
        {
            _db.Remove(match);

            await _db.SaveChangesAsync();

            return true;
        }

        return false;
    }

    private async Task<bool> RemoveCompanyParticipantAsync(Guid id)
    {
        var match = await _db.Set<ParticipantCompany>().FirstOrDefaultAsync(x => x.Id == id);

        if (match != null)
        {
            _db.Remove(match);

            await _db.SaveChangesAsync();

            return true;
        }

        return false;
    }

    private async Task<ApiResponseMessage> EditPersonParticipantAsync(ParticipantPersonDto participant, Guid id)
    {
        var match = await _db.Set<ParticipantPerson>().FirstOrDefaultAsync(x => x.Id == id);

        if (match != null)
        {
            match.FirstName = participant.FirstName;
            match.LastName = participant.LastName;
            match.IdCode = participant.IdCode;
            
            await _db.SaveChangesAsync();

            return new ApiResponseMessage("Participant updated successfully");
        }

        return new ApiResponseMessage(isSuccess: false, "Did not find given participant");
    }

    private async Task<ApiResponseMessage> EditCompanyParticipantAsync(ParticipantCompanyDto participant, Guid id)
    {
        var match = await _db.Set<ParticipantCompany>().FirstOrDefaultAsync(x => x.Id == id);
        
        if (match != null)
        {
            match.RegistryCode = participant.CompanyRegistryCode;
            match.CompanyName = participant.CompanyName;

            await _db.SaveChangesAsync();

            return new ApiResponseMessage("Participant updated successfully");
        }

        return new ApiResponseMessage(isSuccess: false, "Did not find given participant");
    }

    private async Task<ApiResponseMessage> AddCompanyParticipantToEventAsync(AddParticipantRequest participant, Event givenEvent)
    {

        if (participant.Id != null)
        {
            var company = await _db.Set<ParticipantCompany>()
                .FirstOrDefaultAsync(x => x.Id == participant.Id);

            if (company == null)
            {
                return new ApiResponseMessage("Company does not exist in our database.");
            }

            await _db.AddAsync(
                new ParticipantEntry
                {
                    ParticipantCompanyId = participant.Id,
                    PaymentType = participant.PaymentType,
                    AdditionalInfo = participant.CompanyDto?.AdditionalInfo,
                    ParticipantCount = 1,
                    Event = givenEvent
                });
        }
        else
        {
            var participantId = Guid.NewGuid();
            ParticipantCompany company = new ParticipantCompany()
            {
                Id = participantId,
                CompanyName = participant.CompanyDto.CompanyName,
                RegistryCode = participant.CompanyDto.CompanyRegistryCode,
            };

            await _db.AddAsync(company);

            await _db.AddAsync(
                new ParticipantEntry
                {
                    ParticipantCompanyId = participantId,
                    PaymentType = participant.PaymentType,
                    AdditionalInfo = participant.CompanyDto.AdditionalInfo,
                    ParticipantCount = participant.CompanyDto.AmountOfParticipants,
                    Event = givenEvent
                });
        }

        await _db.SaveChangesAsync();

        return new ApiResponseMessage(isSuccess: true, "Participant successfully added to event");
    }

    private async Task<ApiResponseMessage> AddPersonParticipantToEventAsync(AddParticipantRequest participant, Event givenEvent)
    {

        if (participant.Id != null)
        {
            var person = await _db.Set<ParticipantPerson>()
                .FirstOrDefaultAsync(x => x.Id == participant.Id);

            if (person == null)
            {
                return new ApiResponseMessage("Person does not exist in our database.");
            }

            await _db.AddAsync(
                new ParticipantEntry
                {
                    ParticipantPersonId = participant.Id,
                    PaymentType = participant.PaymentType,
                    AdditionalInfo = participant.PersonDto?.AdditionalInfo,
                    ParticipantCount = 1,
                    Event = givenEvent
                });
        }
        else
        {
            var participantId = Guid.NewGuid();
            ParticipantPerson person = new ParticipantPerson()
            {
                Id = participantId,
                FirstName = participant.PersonDto.FirstName,
                LastName = participant.PersonDto.LastName,
                IdCode = participant.PersonDto.IdCode,
            };

            await _db.AddAsync(person);

            await _db.AddAsync(
                new ParticipantEntry
                {
                    ParticipantPersonId = participantId,
                    PaymentType = participant.PaymentType,
                    AdditionalInfo = participant.PersonDto?.AdditionalInfo,
                    ParticipantCount = 1,
                    Event = givenEvent
                });
        }

        await _db.SaveChangesAsync();

        return new ApiResponseMessage(isSuccess: true, "Participant successfully added to event");
    }
}
