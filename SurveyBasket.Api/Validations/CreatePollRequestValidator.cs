
namespace SurveyBasket.Api.Validations;

public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
{
    public CreatePollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 100)
            .WithMessage("{PropertyName} should be between {MinLength} and {MaxLength} characters. You entered [{PropertyValue}]");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(3, 500)
            .WithMessage("{PropertyName} should be between {MinLength} and {MaxLength} characters. You entered [{PropertyValue}]");
    }
}

// Learning-only example (not used by Polls endpoints) — custom .Must() + .When()
public class Student
{
    public string Name { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
}

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.DateOfBirth)
            .Must(BeAtLeast18YearsOld)
            .When(x => x.DateOfBirth.HasValue) // only run custom rule when value exists
            .WithMessage("Student must be at least 18 years old.");
    }

    private static bool BeAtLeast18YearsOld(DateTime? dateOfBirth)
        => DateTime.Today >= dateOfBirth!.Value.AddYears(18);
}

