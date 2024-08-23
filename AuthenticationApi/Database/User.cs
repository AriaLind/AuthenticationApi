using Microsoft.AspNetCore.Identity;

namespace AuthenticationApi.Database;

public class User : IdentityUser
{
    public string? Initials { get; set; }
}