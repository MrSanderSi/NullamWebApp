namespace NullamWebApp.ApiService.Models.Response;

public class ApiResponseMessage
{
    public bool IsSuccess { get; set; } = true;
    public string ResultMessage { get; set; }

    public ApiResponseMessage(bool isSuccess, string resultMessage)
    {
        IsSuccess = isSuccess;
        ResultMessage = resultMessage;
    }

    public ApiResponseMessage(string resultMessage)
    {
        ResultMessage = resultMessage;
    }
}
