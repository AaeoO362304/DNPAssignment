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
        HttpContext context,
        IHttpClientFactory httpClientFactory) =>
    {
        var form = await context.Request.ReadFormAsync();

        string userName = form["userName"].ToString();
        string password = form["password"].ToString();

        LoginRequest request = new(userName, password);

        HttpClient apiClient = httpClientFactory.CreateClient("Api");

        HttpResponseMessage response = await apiClient.PostAsJsonAsync(
            "auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            return Results.Redirect("/login?error=1");
        }

        UserDto? user = await response.Content.ReadFromJsonAsync<UserDto>();

        if (user == null)
        {
            return Results.Redirect("/login?error=1");
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

        return Results.Redirect("/");
    })
    .DisableAntiforgery();



app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();