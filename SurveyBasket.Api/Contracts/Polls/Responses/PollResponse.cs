namespace SurveyBasket.Api.Contracts.Polls.Responses;

public record PollResponse(int Id, string? Title, string? Summary, bool isPublished, DateOnly StartsAt, DateOnly EndsAt);


