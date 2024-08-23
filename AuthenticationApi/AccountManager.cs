using AuthenticationApi.Database;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationApi;

public class AccountManager(SignInManager<User> signInManager, UserManager<User> userManager)
{
    public async Task SignOutAsync()
    {
        var users = userManager.Users.ToList();

        await signInManager.SignOutAsync();
    }
}
