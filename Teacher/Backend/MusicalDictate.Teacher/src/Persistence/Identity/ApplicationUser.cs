using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity;

public class ApplicationUser : IdentityUser
{
    public string UserType { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}