using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<Token>
{
    public RefreshTokenCommand(string? accessToken, string? refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string? AccessToken { get; }
    public string? RefreshToken { get; }
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Token>
{
    private readonly IIdentityService _identityService;

    public RefreshTokenCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Token> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RefreshToken(new Token
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        });
    }
}