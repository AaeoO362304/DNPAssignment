using ApiContracts;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;
    
    public HttpUserService(HttpClient client)
        {
        this.client = client;
        }

    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("users/", request);
        
        response.EnsureSuccessStatusCode();
        
        UserDto? user =  await response.Content.ReadFromJsonAsync<UserDto>();
        
        return user ?? throw new InvalidOperationException("Empty response");
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto request)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync("users/" + id, request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync("users/" + id);
        
        response.EnsureSuccessStatusCode();
    }
}