using FluentValidation;

namespace Application.Assignments.Commands.CreateAssignment;

public class CreateAssignmentCommandValidator : AbstractValidator<CreateAssignmentCommand>
{
    public CreateAssignmentCommandValidator()
    {
        RuleFor(x => x.ExerciseId)
            .NotEmpty().WithMessage("ExerciseId is required.");
    }
}