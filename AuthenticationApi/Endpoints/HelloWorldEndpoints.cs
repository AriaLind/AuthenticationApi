namespace AuthenticationApi.Endpoints;

public static class HelloWorldEndpoints
{
    public static void MapHelloWorldEndpoints(this IEndpointRouteBuilder routes)
    {
        var endpoints = routes.MapGroup("api/hello-world").RequireAuthorization();

        endpoints.MapGet("/", () => "Hello World");
    }
}