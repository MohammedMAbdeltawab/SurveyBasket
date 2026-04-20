

namespace SurveyBasket.Api.Contracts.Polls.Validations;

public class CreatePollRequestValidator:AbstractValidator<PollRequest>
{
    public CreatePollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title Cann't Be Empty")
            .Length(3, 100).WithMessage("Title must be between 3 and 100 characters");   
        RuleFor(x=>x.Summary)
            .NotEmpty().WithMessage("Description Cann't Be Empty")
            .Length(5, 1500).WithMessage("Description must be between 5 and 500 characters");

        RuleFor(x => x.StartsAt)
            .NotEmpty().WithMessage("Start Date Cann't Be Empty")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
        RuleFor(x => x)
            .NotEmpty().WithMessage("End Date Cann't Be Empty")
            .Must(HasValidDates).WithMessage("End Date must be greater than or equal to Start Date");
    }

    private bool HasValidDates(PollRequest poll)
    {
        return poll.EndsAt >= poll.StartsAt;
    }
}
