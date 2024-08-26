using Microsoft.JSInterop;

namespace Blazor.Services;

public class NavigationInterceptor(IJSRuntime jsRuntime)
{
    public async Task OnNavigateAsync()
    {
        await jsRuntime.InvokeVoidAsync("onRouteChange");
    }
}
