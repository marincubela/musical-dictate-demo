using FluentValidation;

namespace Application.Exercises.Commands.DeleteExercise;

public class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
{
    public DeleteExerciseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}