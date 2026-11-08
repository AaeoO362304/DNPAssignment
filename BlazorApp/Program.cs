using BlazorApp.Components;
using BlazorApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication;
using ApiContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5247");
});

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5247")
});

builder.Services.AddScoped<IUserService, HttpUserService>();
builder.Services.AddScoped<IPostService, HttpPostService>();
builder.Services.AddScoped<ICommentService, HttpCommentService>();

builder.Services.AddAuthentication(
        CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found",
    createScopeForStatusCodePages: true);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();


app.MapPost("/session/login", async (
    LoginRequest request,
    IHttpClientFactory httpClientFactory,
    HttpContext context) =>
{
    HttpClient apiClient = httpClientFactory.CreateClient("Api");

    HttpResponseMessage response = await apiClient.PostAsJsonAsync(
        "auth/login", request);

    if (!response.IsSuccessStatusCode)
    {
        return Results.Unauthorized();
    }

    UserDto? user = await response.Content.ReadFromJsonAsync<UserDto>();

    if (user == null)
    {
        return Results.Unauthorized();
    }

    List<Claim> claims = new()
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim("Id", user.Id.ToString())
    };

    ClaimsIdentity identity = new(
        claims,
        CookieAuthenticationDefaults.AuthenticationScheme);

    ClaimsPrincipal principal = new(identity);

    await context.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal);

    return Results.Ok();
});


app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();