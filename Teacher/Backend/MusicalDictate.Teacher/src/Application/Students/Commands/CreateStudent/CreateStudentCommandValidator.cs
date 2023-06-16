using FluentValidation;

namespace Application.Students.Commands.CreateStudent;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email has to be valid.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password  is required.");
    }
}