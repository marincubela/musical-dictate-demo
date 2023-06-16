using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.Commands.LoginStudent;

public class LoginStudentCommand : IRequest<Token>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginStudentCommandHandler : IRequestHandler<LoginStudentCommand, Token>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public LoginStudentCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<Token> Handle(LoginStudentCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.LoginStudent(request.Email, request.Password);

        if (token == null)
            throw new UnauthorizedException("Incorrect email or password");

        return token;
    }
}