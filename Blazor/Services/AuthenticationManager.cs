using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TokenHandler = Blazor.Handlers.TokenHandler;

namespace Blazor.Services;

public class AuthenticationManager(IHttpClientFactory httpClientFactory, UserService userService, AuthenticationStateProvider authenticationStateProvider)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Auth");

    public async Task<SecurityToken?> LogInAsync(string email, string password, bool useCookies, bool useSessionCookies)
    {
        var response = await _httpClient.PostAsJsonAsync($"login?useCookies={useCookies}&useSessionCookies={useSessionCookies}", new { email, password });

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Login failed");
        }

        var json = await response.Content.ReadAsStringAsync();

        var token = JsonConvert.DeserializeObject<SecurityToken>(json);

        TokenHandler.SecurityToken = token;

        var name = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Email, email)
        }, "cookie");
        userService.SetUser(new ClaimsPrincipal(name));

        await authenticationStateProvider.GetAuthenticationStateAsync();

        return token ?? null;
    }

    public async Task LogOutAsync()
    {
        var response = await _httpClient.PostAsync("logout", null);

        userService.SetUser(new ClaimsPrincipal(new ClaimsIdentity()));

        await authenticationStateProvider.GetAuthenticationStateAsync();
    }
}


public class Token
{
    public string? TokenType { get; set; }
    public string? AccessToken { get; set; }
    public int? ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
}