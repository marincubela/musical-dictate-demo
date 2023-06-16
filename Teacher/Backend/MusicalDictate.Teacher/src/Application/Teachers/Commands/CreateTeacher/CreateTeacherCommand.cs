using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Teachers.Commands.CreateTeacher;

public class CreateTeacherCommand : IRequest<Token>
{
    public string Email { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string Password { get; set; }
}

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Token>
{
    private readonly IIdentityService _identityService;

    public CreateTeacherCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Token> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.CreateTeacher(request.Email, request.FirstName, request.LastName, request.Password, cancellationToken);

        return token;
    }
}