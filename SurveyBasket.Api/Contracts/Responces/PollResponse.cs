namespace SurveyBasket.Api.Contracts.Responces;

public record PollResponse(
    int Id,
    string Title, 
    //string Description
    string Notes  // to test the mapping of the new property
    );
