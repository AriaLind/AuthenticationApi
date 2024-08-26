using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace Blazor.Services;

public class AuthenticationManager(IHttpClientFactory httpClientFactory, UserService userService, AuthenticationStateProvider authenticationStateProvider)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Auth");

    public async Task<Token?> LogInAsync(string email, string password, bool useCookies, bool useSessionCookies)
    {
        var response = await _httpClient.PostAsJsonAsync($"login?useCookies={useCookies}&useSessionCookies={useSessionCookies}", new { email, password });

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Login failed");
        }

        var json = await response.Content.ReadAsStringAsync();

        var token = JsonConvert.DeserializeObject<Token>(json);

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