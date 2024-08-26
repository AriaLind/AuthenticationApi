using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Blazor.Services;
using TokenHandler = Blazor.Handlers.TokenHandler;

namespace Blazor.Middleware;

public class TokenValidationMiddleware(
    RequestDelegate next,
    TokenValidationParameters tokenValidationParameters,
    ISecurityTokenValidator tokenValidator,
    ILogger<TokenValidationMiddleware> logger,
    AuthenticationManager authenticationManager)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var token = TokenHandler.SecurityToken;

            if (!string.IsNullOrEmpty(token.Id))
            {
                try
                {
                    var test = tokenValidator.ValidateToken(token.SecurityKey.ToString(), new TokenValidationParameters(), out var validatedToken);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    //var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                    // Attach the user principal to the HttpContext
                    //context.User = principal;
                }
                catch (SecurityTokenException ex)
                {
                    logger.LogError($"Token validation failed: {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Token validation failed.");
                    return;
                }
                catch (Exception ex)
                {
                    logger.LogError($"Unexpected error during token validation: {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("An error occurred while processing your request.");
                    return;
                }
            }
        }

        // Continue processing the request if token validation passes or if there's no token
        await next(context);
    }
}