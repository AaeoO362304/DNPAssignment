using ApiContracts;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;
    
    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<PostDto> AddPostAsync(CreatePostDto request)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("posts/", request);
        
        response.EnsureSuccessStatusCode();
        
        PostDto? post =  await response.Content.ReadFromJsonAsync<PostDto>();
        
        return post ?? throw new InvalidOperationException("Empty response");
    }

    public Task<PostDto> AddUserAsync(CreatePostDto request)
    {
        throw new NotImplementedException();
    }

    public async Task UpdatePostAsync(int id, UpdatePostDto request)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync("posts/" + id, request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePostAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync("posts/" + id);
        
        response.EnsureSuccessStatusCode();
    }
}