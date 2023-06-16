using FluentValidation;

namespace Application.Assignments.Queries.GetAssignment;

public class GetAssignmentQueryValidator : AbstractValidator<GetAssignmentQuery>
{
    public GetAssignmentQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}