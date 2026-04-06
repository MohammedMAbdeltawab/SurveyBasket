namespace SurveyBasket.Api.Contracts.Polls.Requests;

public class CreatePollRequest
{
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
