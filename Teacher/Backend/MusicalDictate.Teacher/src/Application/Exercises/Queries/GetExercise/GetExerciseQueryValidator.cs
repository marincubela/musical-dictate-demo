using FluentValidation;

namespace Application.Exercises.Queries.GetExercise;

public class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
{
    public GetExerciseQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}