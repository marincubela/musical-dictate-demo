using System.Security.Claims;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public IdentityService(UserManager<ApplicationUser> userManager, IApplicationDbContext context, ITokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<string> GetUserNameAsync(string id)
    {
        var user = await _userManager.Users
            .FirstAsync(user => user.Id == id);

        return user.UserName!;
    }

    public async Task<Token> CreateStudent(string email, string jmbag, string firstName, string lastName, string nameClass, string password, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser {UserType = "Student", Email = email, UserName = email};

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception();

        await _userManager.AddToRoleAsync(user, "Student");

        var student = Student.Create(user.Id, jmbag, firstName, lastName, nameClass);

        _context.Students.Add(student);

        await _context.SaveChangesAsync(cancellationToken);

        var token = await GetTokenForUser(user);

        return token;
    }

    public async Task<Token> CreateTeacher(string email, string firstName, string lastName, string password, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser {UserType = "Teacher", Email = email, UserName = email};

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception();

        await _userManager.AddToRoleAsync(user, "Teacher");

        var teacher = new Teacher {Id = user.Id};

        _context.Teachers.Add(teacher);

        await _context.SaveChangesAsync(cancellationToken);

        var token = await GetTokenForUser(user);

        return token;
    }

    public async Task<Token?> LoginStudent(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is not {UserType: "Student"} || !await _userManager.CheckPasswordAsync(user, password))
            return null;

        var token = await GetTokenForUser(user);

        return token;
    }

    public async Task<Token?> LoginTeacher(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is not {UserType: "Teacher"} || !await _userManager.CheckPasswordAsync(user, password))
            return null;

        var token = await GetTokenForUser(user);

        return token;
    }

    public async Task<Token?> LoginGrader(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is not {UserType: "Grader"} || !await _userManager.CheckPasswordAsync(user, password))
            return null;

        var token = await GetTokenForUser(user);

        return token;
    }

    public async Task LogoutUser() { }

    public async Task<Token> RefreshToken(Token token)
    {
        string? accessToken = token.AccessToken;
        string? refreshToken = token.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
            throw new InvalidTokensException();

        var userId = principal.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).SingleOrDefault();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new InvalidTokensException();

        var newAccessToken = _tokenService.GenerateToken(principal.Claims.ToList());
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new Token {AccessToken = newAccessToken, RefreshToken = newRefreshToken};
    }

    public async Task Revoke(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            throw new NotFoundException("User", id);

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
    }

    private async Task<Token> GetTokenForUser(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim> {new(ClaimTypes.NameIdentifier, user.Id)};
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var accessToken = _tokenService.GenerateToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = _tokenService.GenerateRefreshTokenExpiryTime();

        await _userManager.UpdateAsync(user);

        return new Token {AccessToken = accessToken, RefreshToken = refreshToken};
    }
}