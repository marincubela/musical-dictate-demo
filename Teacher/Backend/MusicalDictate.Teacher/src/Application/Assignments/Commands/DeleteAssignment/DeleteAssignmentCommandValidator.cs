using FluentValidation;

namespace Application.Assignments.Commands.DeleteAssignment;

public class DeleteAssignmentCommandValidator : AbstractValidator<DeleteAssignmentCommand>
{
    public DeleteAssignmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}