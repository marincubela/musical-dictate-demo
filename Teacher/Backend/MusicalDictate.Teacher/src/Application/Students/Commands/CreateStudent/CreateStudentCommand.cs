using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Students.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<Token>
{
    public string Email { get; set; }
    
    public string Jmbag { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string NameClass { get; set; }
    
    public string Password { get; set; }
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Token>
{
    private readonly IIdentityService _identityService;

    public CreateStudentCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Token> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.CreateStudent(request.Email, request.Jmbag, request.FirstName, request.LastName, request.NameClass, request.Password, cancellationToken);

        return token;
    }
}