using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Auth.Commands.LoginGrader;

public class LoginGraderCommand : IRequest<Token>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginGraderCommandHandler : IRequestHandler<LoginGraderCommand, Token>
{
    private readonly IIdentityService _identityService;

    public LoginGraderCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Token> Handle(LoginGraderCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.LoginGrader(request.Email, request.Password);

        if (token == null)
            throw new UnauthorizedException("Incorrect email or password");

        return token;
    }
}