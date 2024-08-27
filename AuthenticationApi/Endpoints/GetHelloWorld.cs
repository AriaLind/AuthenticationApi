using System.Net.Http.Headers;

namespace AuthenticationApi.Endpoints;

public static class GetHelloWorld
{
    public static void MapGetHelloWorld(this IEndpointRouteBuilder routes)
    {
        var endpoints = routes.MapGroup("api/get-hello-world");

        endpoints.MapGet("/", async (string token) =>
        {
            HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            HttpClient httpClient = new(handler);

            httpClient.BaseAddress = new Uri("https://localhost:7027");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("/api/hello-world");

            var content = await response.Content.ReadAsStringAsync();

            return content;
        });
    }
}