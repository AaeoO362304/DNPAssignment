using ApiContracts;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;
    
    public HttpUserService(HttpClient client)
        {
        this.client = client;
        }

    public Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        return Task.FromResult(new UserDto());
    }

    public Task UpdateUserAsync(int id, UpdateUserDto request)
    {
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        return Task.CompletedTask;
    }
}