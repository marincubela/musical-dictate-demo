using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IDateService _dateTimeService;

    public TokenService(IConfiguration configuration, IDateService dateTimeService)
    {
        _configuration = configuration;
        _dateTimeService = dateTimeService;
    }

    /// <inheritdoc />
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(_configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            expires: _dateTimeService.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    /// <inheritdoc />
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <inheritdoc />
    public DateTime GenerateRefreshTokenExpiryTime() 
        => DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["JWT:RefreshTokenValidityInDays"]));

        /// <inheritdoc />
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}