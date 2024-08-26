using Microsoft.IdentityModel.Tokens;

namespace Blazor.Handlers;

public static class TokenHandler
{
    public static SecurityToken SecurityToken { get; set; }
}