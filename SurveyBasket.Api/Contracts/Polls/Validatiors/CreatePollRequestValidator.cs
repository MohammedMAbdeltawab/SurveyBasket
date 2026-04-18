

namespace SurveyBasket.Api.Contracts.Polls.Validations;

public class CreatePollRequestValidator:AbstractValidator<CreatePollRequest>
{
    public CreatePollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title Cann't Be Empty")
            .Length(3, 100).WithMessage("Title must be between 3 and 100 characters");   
        RuleFor(x=>x.Description)
            .NotEmpty().WithMessage("Description Cann't Be Empty")
            .Length(5, 500).WithMessage("Description must be between 5 and 500 characters");
    }
}
