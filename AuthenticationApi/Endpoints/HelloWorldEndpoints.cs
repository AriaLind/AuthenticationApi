namespace AuthenticationApi.Endpoints;

public static class HelloWorldEndpoints
{
    public static void MapHelloWorldEndpoints(this IEndpointRouteBuilder routes)
    {
        var endpoints = routes.MapGroup("api/hello-world");

        endpoints.MapGet("/", () => "Hello World");
    }
}