namespace NullamWebApp.Web.Services;

public class ParticipantService
{
	private readonly NullamApiClient _apiClient;

	public ParticipantService(NullamApiClient apiClient)
	{
		_apiClient = apiClient;
	}
}
