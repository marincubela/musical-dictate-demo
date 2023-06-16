using FluentValidation;

namespace Application.Assignments.Commands.UpdateAssignment;

public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
{
    public UpdateAssignmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}