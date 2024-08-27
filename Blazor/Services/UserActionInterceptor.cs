using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;

namespace Blazor.Services;

public class UserActionInterceptor(IJSRuntime jsRuntime)
{
    public async Task OnNavigateAsync()
    {
        var test = await jsRuntime.InvokeAsync<string>("onUserAction");


    }
}
