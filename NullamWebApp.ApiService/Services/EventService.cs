using Microsoft.EntityFrameworkCore;
using NullamWebApp.ApiService.Models;
using NullamWebApp.ApiService.Models.Request;
using NullamWebApp.ApiService.Models.Response;
using NullamWebApp.Data;
using NullamWebApp.Data.Models;
using NullamWebApp.Shared.Models;

namespace NullamWebApp.ApiService.Services;

public class EventService : ServiceBase
{
    private readonly NullamDbContext _db;

    public EventService(NullamDbContext db)
    {
        _db = db;
    }
    public async Task<SingleEventResponse> GetEventAsync(GetEventRequest request)
    {
        var match = await _db.Set<Event>()
            .Include(x => x.Participants)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (match != null)
        {
            var resp = new EventResponse()
            {
                Id = match.Id,
                EventName = match.EventName,
                EventStart = match.EventStarts,
                EventEnd = match.EventEnds,
                Address = match.Address,
                ParticipantCount = match.Participants
                    .Where(y => y.EventId == match.Id)
                    .Sum(x => x.ParticipantCount),
                IsOnline = match.IsOnline,
                AdditionalInfo = match.AdditionalInfo
            };

            return new SingleEventResponse() { EventResponse = resp };
        }

        return new SingleEventResponse() { ResponseMessage = "No event found with given ID", IsSuccess = false };
    }

    public async Task<EventWithParticipantsResponse> GetEventWithParticipantsAsync(GetEventRequest request)
    {
        var match = await _db.Set<Event>()
			.Where(x => x.Id == request.Id)
			.Include(x => x.Participants)
				.ThenInclude(x => x.Person)
			.Include(x => x.Participants)
				.ThenInclude(x => x.Company)
            .Include(x => x.Address)
			.FirstOrDefaultAsync();

        if (match != null)
        {
            var result = new EventWithParticipantsResponse()
            {
                Event = new EventResponse()
                {
                    EventName = match.EventName,
                    EventStart = match.EventStarts,
                    EventEnd = match.EventEnds,
                    AdditionalInfo = match.AdditionalInfo,
                    Address = match.Address,
                    IsOnline = match.IsOnline,
                    ParticipantCount = match.Participants?.Count() ?? 0
                }
            };

			if (match.Participants != null)
			{
				foreach (var participant in match.Participants)
				{
					if (participant.ParticipantPersonId != null)
					{
						var person = participant.Person;

						result.ParticipantPeople.Add(new ParticipantPersonResponse()
						{
							FirstName = person.FirstName,
							LastName = person.LastName,
							IdCode = person.IdCode,
							AdditionalInfo = participant.AdditionalInfo,
							PaymentType = participant.PaymentType
						});
					}
					else if (participant.ParticipantCompanyId != null)
					{
						var company = participant.Company;
						result.ParticipantCompanys.Add(new ParticipantCompanyResponse()
						{
							CompanyName = company.CompanyName,
							CompanyRegistryCode = company.RegistryCode,
							AmountOfParticipants = participant.ParticipantCount,
							AdditionalInfo = participant.AdditionalInfo,
							PaymentType = participant.PaymentType
						});
					}
				}
			}

			return result;
		}

        return new EventWithParticipantsResponse() { IsSuccess = false, ResponseMessage = "Event no longer exists in database"};
    }

	public async Task<EventListResponse> GetAllUpcomingEvents()
    {
        var upcomingEvents = await _db.Set<Event>()
            .Include(x => x.Participants)
            .Where(x => x.EventStarts > DateTimeOffset.Now)
            .OrderBy(x => x.EventStarts)
            .Select(x => new EventResponse()
            {
                Id = x.Id,
                EventName = x.EventName,
                EventStart = x.EventStarts,
                EventEnd = x.EventEnds,
                Address = x.Address,
                IsOnline = x.IsOnline,
                AdditionalInfo = x.AdditionalInfo,
                ParticipantCount = x.Participants
                    .Where(y => y.EventId == x.Id)
                    .Sum(x => x.ParticipantCount),
            })
            .Take(100)
            .ToListAsync();

        return new EventListResponse() { Response = upcomingEvents };
    }

    public async Task<EventListResponse> GetAllPastEvents()
    {
        var pastEvents = await _db.Set<Event>()
            .Include(x => x.Participants)
            .Where(x => x.EventStarts <= DateTimeOffset.Now)
            .OrderByDescending(x => x.EventStarts)
            .Select(x => new EventResponse()
            {
                Id = x.Id,
                EventName = x.EventName,
                EventStart = x.EventStarts,
                EventEnd = x.EventEnds,
                Address = x.Address,
                IsOnline = x.IsOnline,
                AdditionalInfo = x.AdditionalInfo,
                ParticipantCount = x.Participants
                    .Where(y => y.EventId == x.Id)
                    .Sum(x => x.ParticipantCount),
            })
            .Take(100)
            .ToListAsync();

        return new EventListResponse() { Response = pastEvents };
    }

    public async Task<ApiResponseMessage> AddEventAsync(AddEventRequest request)
    {
        var addEvent = new Event()
        {
            EventName = request.EventName,
            EventStarts = request.EventStart,
            EventEnds = request.EventEnd,
            IsOnline = request.IsOnline,
            AdditionalInfo = request.AdditionalInfo,
            Address = MapAddressToDbAddress(request.Address)
        };

        bool existsInDatabase = await _db.Set<Event>()
            .AnyAsync(x => x.EventName == addEvent.EventName &&
            ( x.EventStarts <= addEvent.EventStarts.AddMinutes(5)
            && x.EventStarts >= addEvent.EventStarts.AddMinutes(-5)));

        if (existsInDatabase)
        {
            return new ApiResponseMessage("Üritus on juba andmebaasi registreeritud!");
        }

        await _db.AddAsync(addEvent);
        await _db.SaveChangesAsync();

        return new ApiResponseMessage(isSuccess: true, "Ürituse lisamine andmebaasi õnnestus!");
    }

    public async Task<ApiResponseMessage> DeleteEventAsync(DeleteEventRequest request)
    {
        var eventToRemove = await _db.Set<Event>().FirstOrDefaultAsync(x => x.Id == request.Id);

        if (eventToRemove != null)
        {
            _db.Remove(eventToRemove);

            await _db.SaveChangesAsync();
        }

        return new ApiResponseMessage("Üritus edukalt andmebaasist eemaldatud");
    }

    public async Task<ApiResponseMessage> EditEventAsync(EditEventRequest request)
    {
        var match = await _db.Set<Event>().FirstOrDefaultAsync(x => x.Id == request.EventId);

        if (match != null)
        {
            match.EventName = request.EventName;
            match.EventStarts = request.EventStart;
            match.EventEnds = request.EventEnd;
            match.Address = MapAddressToDbAddress(request.Address);
            match.IsOnline = request.IsOnline;
        }

        await _db.SaveChangesAsync();

        return new ApiResponseMessage("Event edited successfully!");
    }


    private static Data.Models.Address? MapAddressToDbAddress(Shared.Models.Address address)
    {
        if(address == null)
        {
            return null;
        }

        return new Data.Models.Address()
        {
            Country = address.Country,
            County = address.County,
            City = address.City,
            Street = address.Street,
            AdditionalLines = address.AdditionalLines,
            PostalCode = address.PostalCode
        };
    }

    private static Shared.Models.Address? MapDbAddressToAddress(Data.Models.Address address) {
        if (address == null)
        {
            return null;
        }

        return new Shared.Models.Address()
        {
            Country = address.Country,
            County = address.County,
            City = address.City,
            Street = address.Street,
            AdditionalLines = address.AdditionalLines,
            PostalCode = address.PostalCode
        };
    }
}
