namespace SurveyBasket.Api.Contracts.Polls.Requests;

public record PollRequest(string? Title, string? Summary, bool isPublished, DateOnly StartsAt, DateOnly EndsAt);
