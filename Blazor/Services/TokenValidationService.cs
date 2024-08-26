using System.Security.Claims;
using System.Text.Encodings.Web;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blazor.Services;

public class TokenValidationService(ISecurityTokenValidator validator)
{

    private Task<bool> ValidateTokenAsync(string token)
    {
        var test = validator.ValidateToken(token, new TokenValidationParameters(), out _);

        // Implement your token validation logic here
        // For example, call an external service or database to validate the token
        return Task.FromResult(true); // Replace with actual validation
    }
}