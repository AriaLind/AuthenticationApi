using System.Net;
using System.Net.Http.Headers;
using Blazor.Components;
using Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using AuthenticationManager = Blazor.Services.AuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddHttpClient("Auth", client =>
    {
        client.BaseAddress = new Uri("https://localhost:7027"); // Update to your API base address
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        UseCookies = true, // This will use cookies stored by the browser
        CookieContainer = new CookieContainer() // Container to manage cookies
    });

builder.Services.AddScoped<AuthenticationManager>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserActionInterceptor>();


var app = builder.Build();

app.UseAuthentication(); // Make sure this comes before UseAuthorization
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
