using System.Security.Claims;

namespace Application.Common.Interfaces;

public interface ITokenService
{
    public string GenerateToken(IEnumerable<Claim> claims);
    
    public string GenerateRefreshToken();
    
    public DateTime GenerateRefreshTokenExpiryTime();

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? accessToken);
}