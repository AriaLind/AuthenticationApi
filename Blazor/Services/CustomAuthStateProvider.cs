using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blazor.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly UserService _userService;
    public CustomAuthStateProvider(UserService userService)
    {
        _userService = userService;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = _userService.GetUser();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        return Task.FromResult(new AuthenticationState(user));
    }
}