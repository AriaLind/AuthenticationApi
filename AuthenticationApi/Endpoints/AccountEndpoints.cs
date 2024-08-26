namespace AuthenticationApi.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder routes)
    {
        var endpoints = routes.MapGroup("");

        endpoints.MapPost("/logout", (AccountManager accountManager) => Task.FromResult(accountManager.SignOutAsync()));
    }
}